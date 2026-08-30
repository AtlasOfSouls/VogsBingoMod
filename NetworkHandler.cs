/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VogsBingoMod.UI;
using UnityEngine;

namespace VogsBingoMod
{
    internal static class NetworkHandler
    {
        const string startOfBingosyncRoomLink = "https://bingosync.com/room/";
        const string startOfCaravanRoomLink = "https://caravan.kobold60.com/room/";
        const float WSCheckInterval = 5;
        const int defaultTimeout = 15000;
        internal static ConnectionState connectState {get; private set;} = ConnectionState.NotConnected;
        internal static bool CanConnectToRoom => connectState == ConnectionState.NotConnected;
        internal static bool CanDisconnectFromRoom => connectState == ConnectionState.Connected;
        static HttpClient httpClient = new HttpClient() {Timeout = TimeSpan.FromSeconds(15)};
        static ClientWebSocket? webSocketClient;
        static string socketKeyJson = string.Empty;
        static string roomCode = string.Empty;
        static byte[] receivedWebSocketMessage = new byte[1024];
        static UTF8Encoding utf8 = new UTF8Encoding(false, true);
        static string domainName = "";
        static string socketDomainName = "";
        static HttpTaskList CurrentHttpTasks = new HttpTaskList();
        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        static float timer = 0;
        internal static RoomType roomType {get; private set;} = RoomType.Bingosync;

        internal static void Update()
        {
            if (timer <= 0)
            {
                timer = WSCheckInterval;
                CurrentHttpTasks.CheckForTimeouts();
            } else
            {
                timer -= Time.unscaledDeltaTime;
            }
        }

        internal static void SendRevealCardMessage()
        {
            if (connectState == ConnectionState.Connected)
            {
                string revealCardJson = JsonHelper.CreateRevealCardJson(roomCode);
                StringContent postReqContent = new StringContent(revealCardJson, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage>? revealCardTask = SendPostRequest($"https://{domainName}.com/api/revealed", postReqContent, true);
                if (revealCardTask != null)
                {
                    revealCardTask.ContinueWith(VerifyReveal);
                }
            }
        }

        static void VerifyReveal(Task<HttpResponseMessage> revealCardTask)
        {
            HttpResponseMessage revealCardResponse = revealCardTask.Result;
            CurrentHttpTasks.Remove(revealCardTask);
            try{
                revealCardResponse.EnsureSuccessStatusCode();
            } catch (Exception e){
                VogsBingoModPlugin.LogError($"{e.Message}");
                Coroutiner.CreateCoroutine(UIHelper.TriggerErrorText_Main());
            }
            Task<string> readResponse = revealCardResponse.Content.ReadAsStringAsync();
            readResponse.ContinueWith(LogComplete);
        }

        internal static void SetMyColor(int newColorID)
        {
            if (connectState == ConnectionState.Connected)
            {
                string switchColorJson = JsonHelper.CreateColorSwitchJson(newColorID, roomCode, roomType);
                StringContent postReqContent = new StringContent(switchColorJson, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage>? switchColorTask = SendPostRequest($"https://{domainName}.com/api/color", postReqContent, true);
                if (switchColorTask != null)
                {
                    switchColorTask.ContinueWith(PostColorSwitch);
                }
            }
        }

        static void PostColorSwitch(Task<HttpResponseMessage> switchColorTask)
        {
            HttpResponseMessage switchColorResponse = switchColorTask.Result;
            CurrentHttpTasks.Remove(switchColorTask);
            try{
            switchColorResponse.EnsureSuccessStatusCode();
            } catch (Exception e){
                VogsBingoModPlugin.LogError($"{e.Message}");
                Coroutiner.CreateCoroutine(UIHelper.TriggerErrorText_Main());
            }
            Task<string> readResponse = switchColorResponse.Content.ReadAsStringAsync();
            readResponse.ContinueWith(LogComplete);

        }

        internal static void MarkGoal(int slotIndex, bool remove, string color)
        {
            if (connectState == ConnectionState.Connected)
            {
                string goalMarkJson = JsonHelper.CreateGoalMarkJson(slotIndex+1, roomCode, color, remove, roomType);
                StringContent postReqContent = new StringContent(goalMarkJson, Encoding.UTF8, "application/json");
                try{
                    Task<HttpResponseMessage>? markTask = SendPostRequest($"https://{domainName}.com/api/select", postReqContent, true);
                    if (markTask != null)
                    {
                        markTask.ContinueWith(PostMark);
                    }
                } catch (Exception e)
                {
                    VogsBingoModPlugin.LogError(e);
                }
            }
        }

        static void PostMark(Task<HttpResponseMessage> markTask)
        {
            HttpResponseMessage markResponse = markTask.Result;
            CurrentHttpTasks.Remove(markTask);
            bool flag = false;
            try{
            markResponse.EnsureSuccessStatusCode();
            } catch (Exception){
                flag = true;
            }
            Task<string> readResponse = markResponse.Content.ReadAsStringAsync();
            readResponse.ContinueWith(CheckForLockoutBlock, flag);
        }

        static void CheckForLockoutBlock(Task<string> task, object data)
        {
            bool failedToMark = (bool)data;
            string result = task.Result;
            task.Dispose();
            if (failedToMark && !result.Equals("Blocked by Lockout"))
            {
                Coroutiner.CreateCoroutine(UIHelper.TriggerErrorText_Main());
            } else if (failedToMark)
            {
                VogsBingoModPlugin.LogInfo("Mark was blocked by lockout.");
            }
        }

        static void LogComplete(Task<string> task)
        {
            task.Dispose();
        }

        internal static void JoinRoom(string roomUrl, string nicknameInput, string passwordInput)
        {
            if (connectState == ConnectionState.NotConnected) {
                if (roomUrl.Length > startOfBingosyncRoomLink.Length && roomUrl.Substring(0, startOfBingosyncRoomLink.Length).Equals(startOfBingosyncRoomLink))
                {
                    VogsBingoModPlugin.LogInfo("Attempting to connect to bingosync room...");
                    roomCode = roomUrl.Substring(startOfBingosyncRoomLink.Length);
                    domainName = "www.bingosync";
                    socketDomainName = "bingosync";
                    roomType = RoomType.Bingosync;
                } else if (roomUrl.Length > startOfCaravanRoomLink.Length && roomUrl.Substring(0, startOfCaravanRoomLink.Length).Equals(startOfCaravanRoomLink))
                {
                    VogsBingoModPlugin.LogInfo("Attempting to connect to caravan room...");
                    roomCode = roomUrl.Substring(startOfCaravanRoomLink.Length);
                    domainName = "caravan.kobold60";
                    socketDomainName = "kobold60";
                    roomType = RoomType.Caravan;
                } else
                {
                    VogsBingoModPlugin.LogInfo("The room link that was entered could not be recognized.");
                    return;
                }
                connectState = ConnectionState.Connecting;
                Coroutiner.CreateCoroutine(UIHelper.NotifyOfConnectingToRoom_Main());
                string jsonString = 
                $"{{\"room\": \"{roomCode}\",\n\"nickname\": \"{nicknameInput}\",\n\"password\": \"{passwordInput}\"}}";
                StringContent postReqContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                try{
                    Task<HttpResponseMessage>? httpPostReqTask = SendPostRequest($"https://{domainName}.com/api/join-room", postReqContent, false);
                    if (httpPostReqTask != null)
                    {
                        httpPostReqTask.ContinueWith(ReadSocketKey);
                    }
                } catch (Exception e)
                {
                    VogsBingoModPlugin.LogError(e);
                }
            }
        }

        static void ReadSocketKey(Task<HttpResponseMessage> httpPostReqTask)
        {
            HttpResponseMessage connectResponse = httpPostReqTask.Result;
            CurrentHttpTasks.Remove(httpPostReqTask);
            try{
                connectResponse.EnsureSuccessStatusCode();
            } catch (Exception)
            {
                connectState = ConnectionState.NotConnected;
                Coroutiner.CreateCoroutine(UIHelper.NotifyOfConnectingToRoomCancel_Main(0));
                return;
            }
            try{
            Task<string> readSocketKeyTask = connectResponse.Content.ReadAsStringAsync();
            readSocketKeyTask.ContinueWith(StartWebSocket);
            }catch (Exception e)
            {
                VogsBingoModPlugin.LogError(e);
            }
        }

        static void StartWebSocket(Task<string> readSocketKeyTask)
        {
            VogsBingoModPlugin.LogInfo("Starting WS...");
            socketKeyJson = readSocketKeyTask.Result;
            readSocketKeyTask.Dispose();
            webSocketClient = new ClientWebSocket();
            Uri socketUri = new Uri($"wss://sockets.{socketDomainName}.com/broadcast");
            try{
            Task wsConnectTask = webSocketClient.ConnectAsync(socketUri, cancellationTokenSource.Token);
            wsConnectTask.ContinueWith(SendJoinViaWebSocket);
            } catch (Exception e)
            {
                VogsBingoModPlugin.LogError(e);
                Coroutiner.CreateCoroutine(UIHelper.NotifyOfConnectingToRoomCancel_Main(1));
            }
        }

        static void SendJoinViaWebSocket(Task wsTask)
        {
            byte[] bytes = utf8.GetBytes(socketKeyJson);
            if (webSocketClient != null)
            {
                try{
                    wsTask = webSocketClient.SendAsync(bytes, WebSocketMessageType.Text, true, cancellationTokenSource.Token);
                } catch (Exception e)
                {
                    VogsBingoModPlugin.LogError(e);
                }
                socketKeyJson = string.Empty;
            }
            wsTask.ContinueWith(PostJoin);
        }

        static void PostJoin(Task wsTask)
        {
            wsTask.Dispose();
            StartListeningForMessages();
            connectState = ConnectionState.Connected;
            Coroutiner.CreateCoroutine(UIHelper.NotifyOfRoomEnter_Main(roomType));
            RequestBoardViaHttp();
        }

        
        internal static void ExitRoom()
        {
            if (connectState == ConnectionState.Connected && webSocketClient != null)
            {
                roomCode = string.Empty;
                domainName = string.Empty;
                socketDomainName = string.Empty;
                VogsBingoModPlugin.LogInfo("Starting the process of leaving the room...");
                connectState = ConnectionState.Disconnecting;
                try{
                    Task wsTask = webSocketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "Exit Room button", CancellationToken.None);
                    CurrentHttpTasks.SetIsExitingRoom(true);
                    wsTask.ContinueWith(PostExit);
                } catch (Exception e)
                {
                    VogsBingoModPlugin.LogError(e);
                    ForceExitRoomWithDisconnectError(2);
                }
            }
        }

        static void PostExit(Task wsTask)
        {
            CurrentHttpTasks.SetIsExitingRoom(false);
            wsTask.Dispose();
            CancelHttpTasks();
            if (webSocketClient != null){
                webSocketClient.Dispose();
            }
            webSocketClient = null;
            connectState = ConnectionState.NotConnected;
            Coroutiner.CreateCoroutine(UIHelper.NotifyOfRoomExit_Main());
            VogsBingoModPlugin.LogInfo("The room has been exited.");
        }

        internal static void RequestBoardViaHttp()
        {
            try{
                Task<HttpResponseMessage>? httpGetReqTask = SendGetRequest($"https://{domainName}.com/room/{roomCode}/board", true);
                if (httpGetReqTask != null)
                {
                    httpGetReqTask.ContinueWith(ReadBoard);
                }
            } catch (Exception e)
            {
                VogsBingoModPlugin.LogError(e);
            }
        }

        static void ReadBoard(Task<HttpResponseMessage> httpGetReqTask)
        {
            HttpResponseMessage boardResponse = httpGetReqTask.Result;
            CurrentHttpTasks.Remove(httpGetReqTask);
            try{
                boardResponse.EnsureSuccessStatusCode();
            } catch (Exception e)
            {
                VogsBingoModPlugin.LogError($"Error requesting the board: {e.Message}");
            }
            Task<string> readBoardTask = boardResponse.Content.ReadAsStringAsync();
            readBoardTask.ContinueWith(SendBoardToUI);
        }

        static void SendBoardToUI(Task<string> readBoardTask)
        {
            string boardJson = readBoardTask.Result;
            readBoardTask.Dispose();
            Coroutiner.CreateCoroutine(UIHelper.SetBoard_Main(boardJson, roomType));
        }

        static void StartListeningForMessages()
        {
            if (webSocketClient != null)
            {
                try{
                    Task<WebSocketReceiveResult> wsReceiveTask = webSocketClient.ReceiveAsync(receivedWebSocketMessage, cancellationTokenSource.Token);
                    wsReceiveTask.ContinueWith(ReceiveWSMessage);
                } catch (Exception e)
                {
                    VogsBingoModPlugin.LogError(e);
                }
            }
        }

        static void ReceiveWSMessage(Task<WebSocketReceiveResult> wsTask)
        {
            WebSocketReceiveResult result = wsTask.Result;
            if (result.MessageType == WebSocketMessageType.Text)
            {
                string jsonStr = Encoding.UTF8.GetString(receivedWebSocketMessage);
                switch (JsonHelper.GetStringValueOfKey("type", jsonStr))
                {
                    case "goal":
                        HandleGoalMessage(jsonStr);
                        break;
                    case "new-card":
                        HandleNewCardMessage();
                        break;
                    default:
                        break;
                }
            }
            Array.Clear(receivedWebSocketMessage, 0, receivedWebSocketMessage.Length);
            if (webSocketClient != null)
            {
                try{
                    wsTask = webSocketClient.ReceiveAsync(receivedWebSocketMessage, cancellationTokenSource.Token);
                    wsTask.ContinueWith(ReceiveWSMessage);
                } catch (Exception e)
                {
                    VogsBingoModPlugin.LogError(e);
                }
            } else
            {
                wsTask.Dispose();
            }
        }

        static void HandleGoalMessage(string jsonStr)
        {
            int colorID = GoalColors.NameToID(JsonHelper.GetStringValueOfKey("color", jsonStr));
            bool remove = JsonHelper.GetBoolValueOfKey("remove", jsonStr);
            int slotIndex = int.Parse(JsonHelper.GetStringValueOfKey("slot", JsonHelper.GetObjectValueOfKey("square", jsonStr)).Substring(4)) - 1;
            try{
                Coroutiner.CreateCoroutine(UIHelper.UpdateGoal_Main(colorID, remove, slotIndex));
            } catch (Exception e)
            {
                VogsBingoModPlugin.LogError(e);
            }
        }

        static void HandleNewCardMessage()
        {
            Coroutiner.CreateCoroutine(UIHelper.UnrevealCard_Main());
            Coroutiner.CreateCoroutine(RequestNewBoardAfterWaiting());
        }

        static IEnumerator RequestNewBoardAfterWaiting()
        {
            yield return new WaitForSecondsRealtime(1);
            RequestBoardViaHttp();
        }

        internal static void Dispose()
        {
            httpClient.Dispose();
            cancellationTokenSource.Dispose();
            if (webSocketClient != null){
                webSocketClient.Dispose();
            }
        }

        static Task<HttpResponseMessage>? SendPostRequest(string uri, HttpContent content, bool requiresWS)
        {
            if (!requiresWS || (webSocketClient != null && webSocketClient.State == WebSocketState.Open))
            {
                try{
                    CancellationTokenSource tokenSource = new CancellationTokenSource();
                    tokenSource.CancelAfter(defaultTimeout);
                    Task<HttpResponseMessage> newTask = httpClient.PostAsync(uri, content, tokenSource.Token);
                    CurrentHttpTasks.Add(newTask, tokenSource);
                    return newTask;
                } catch (Exception e)
                {
                    VogsBingoModPlugin.LogError(e);
                }
            }
            ForceExitRoomWithDisconnectError(3);
            return null;
        }

        static void ForceExitRoomWithDisconnectError(int errorCode)
        {
            cancellationTokenSource.Dispose();
            cancellationTokenSource = new CancellationTokenSource();
            VogsBingoModPlugin.LogInfo("Forcing disconnect");
            if (webSocketClient != null){
                webSocketClient.Dispose();
                webSocketClient = null;
            }
            CancelHttpTasks();
            connectState = ConnectionState.NotConnected;
            CurrentHttpTasks.SetIsExitingRoom(false);
            Coroutiner.CreateCoroutine(UIHelper.NotifyOfRoomExit_Main(errorCode));
        }

        static void CancelHttpTasks()
        {
            httpClient.CancelPendingRequests();
            CurrentHttpTasks.Clear();
        }

        static Task<HttpResponseMessage>? SendGetRequest(string uri, bool requiresWS)
        {
            if (!requiresWS || (webSocketClient != null && webSocketClient.State == WebSocketState.Open))
            {
                try{
                    CancellationTokenSource tokenSource = new CancellationTokenSource();
                    tokenSource.CancelAfter(defaultTimeout);
                    Task<HttpResponseMessage> httpTask = httpClient.GetAsync(uri, tokenSource.Token);
                    CurrentHttpTasks.Add(httpTask, tokenSource);
                    return httpTask;
                } catch (Exception e)
                {
                    VogsBingoModPlugin.LogError(e);
                }
            }
            ForceExitRoomWithDisconnectError(4);
            return null;
        }

        class HttpTaskList : Dictionary<Task<HttpResponseMessage>, CancellationTokenSource>
        {
            bool isExitingRoom = false;
            internal new void Add(Task<HttpResponseMessage> task, CancellationTokenSource tokenSource)
            {
                base.Add(task, tokenSource);
                Coroutiner.CreateCoroutine(UIHelper.SetConnectionPendingActive_Main(true));
            }

            internal new bool Remove(Task<HttpResponseMessage> task)
            {
                if (this.ContainsKey(task))
                {
                    this[task].Cancel();
                    this[task].Dispose();
                    task.Dispose();
                    bool result = base.Remove(task);
                    Coroutiner.CreateCoroutine(UIHelper.SetConnectionPendingActive_Main(this.Count > 0 || isExitingRoom));
                    return result;
                }
                return false;
            }

            internal new void Clear()
            {
                foreach (KeyValuePair<Task<HttpResponseMessage>, CancellationTokenSource> pair in this)
                {
                    pair.Value.Cancel();
                    pair.Value.Dispose();
                    pair.Key.Dispose();
                }
                base.Clear();
                Coroutiner.CreateCoroutine(UIHelper.SetConnectionPendingActive_Main(isExitingRoom));
            }

            internal void SetIsExitingRoom(bool isExitingRoom)
            {
                this.isExitingRoom = isExitingRoom;
                if (isExitingRoom)
                {
                    Coroutiner.CreateCoroutine(UIHelper.SetConnectionPendingActive_Main(true));
                } else
                {
                    Coroutiner.CreateCoroutine(UIHelper.SetConnectionPendingActive_Main(this.Count > 0));
                }
            }

            internal void CheckForTimeouts()
            {
                bool flag = false;
                foreach (CancellationTokenSource tokenSource in this.Values)
                {
                    if (tokenSource.IsCancellationRequested)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    VogsBingoModPlugin.LogInfo("CANCEL REQUESTED");
                    ForceExitRoomWithDisconnectError(1);
                }
            }
        }
    }
}
