using MySqlConnector;
using OAI_API.Configure;
using OAI_API.Models;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace OAI_API.Repositories
{
    public class IARepository : IAIRepository
    {

        private readonly string _address;
        private readonly int _port;


        public IARepository(IConfigService config)
        {
            (_address, _port) = config.GetAddressPort();
        }

        public async Task<AnswerDTO> GetAnswerAsync(string question)
        {
            // target address + port
            var targetAddress = IPAddress.Parse(_address);
            var targetEndpoint = new IPEndPoint(targetAddress, _port);
            
            // create a socket and connect
            var socket = new Socket(targetEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(targetEndpoint);

            // serialize the question
            var mydict = new Dictionary<string, string>()
            {
                ["question"] = question
            };
            var jsonRequest = JsonSerializer.Serialize(mydict);
            var reqBytes = Encoding.UTF8.GetBytes(jsonRequest);
            
            // send the question
            await socket.SendAsync(reqBytes, SocketFlags.None);

            // buffer for the response
            byte[] buffer = new byte[1024];

            // receive some data
            int bytesReceived = socket.Receive(buffer);
            var jsonResp = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

            // deserialize the obj
            var dto = JsonSerializer.Deserialize<AnswerDTO>(jsonResp)!;

            return dto;
        }
    }
}
