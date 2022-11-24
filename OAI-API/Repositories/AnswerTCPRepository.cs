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
    public class AnswerTCPRepository : IAnswerRepository
    {

        private readonly string address;
        private readonly int port;


        public AnswerTCPRepository(IConfigService config)
        {
            (address, port) = config.GetAddressPort();
        }

        public async Task<AnswerDTO> GetAnswerAsync(int answerId)
        {
            throw new NotImplementedException();
        }

        public async Task<AnswerDTO> GetAnswerAsync(string[] answerKeyWords)
        {
            // target address + port
            var targetAddress = IPAddress.Parse(address);
            var targetEndpoint = new IPEndPoint(targetAddress, port);
            
            // create a socket and connect
            var socket = new Socket(targetEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(targetEndpoint);

            // serialize the question
            var mydict = new Dictionary<string, string[]>()
            {
                ["question"] = answerKeyWords
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
