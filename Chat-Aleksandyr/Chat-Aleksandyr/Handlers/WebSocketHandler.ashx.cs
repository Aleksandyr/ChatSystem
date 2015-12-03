using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace Chat_Aleksandyr.Handlers
{
    public class WebSocketHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessWS);
            }
        }

        #endregion

        public static List<WebSocket> sockets = new List<WebSocket>();

        private async Task ProcessWS(AspNetWebSocketContext context)
        {
            WebSocket socket = context.WebSocket;
            sockets.Add(socket);
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
            //WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);

            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult receiveResult = await socket.ReceiveAsync(buffer, CancellationToken.None);
                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    }
                    else if (receiveResult.MessageType == WebSocketMessageType.Text)
                    {
                        string receivedMessage = Encoding.UTF8.GetString(buffer.Array, 0, receiveResult.Count);
                        string userMessage = string.Format("{0}", receivedMessage);
                        buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(userMessage));
                        sockets.ForEach(sck =>
                        {
                            sck.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        });
                    }
                    else
                    {

                    }
                }
            }
            catch
            {

            }
            finally
            {
                sockets.Remove(socket);
                socket.Dispose();
            }
        }
    }
}