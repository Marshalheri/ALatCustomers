using AlatCustomer.Middleware.Core.Services;
using System.Collections.Generic;

namespace AlatCustomer.Middleware.Core.Fakes
{
    public class FakeMessagePackProvider : IMessagePackProvider
    {
        readonly IDictionary<int, MessagePack> _packs;
        public MessagePack GetPack()
        {
            return (_packs.TryGetValue(1, out var pack)) ? pack : null;
        }

        public FakeMessagePackProvider()
        {
            _packs = new Dictionary<int, MessagePack>(1)
            {
                { 1, GetMessagePack() },
            };
        }

        private MessagePack GetMessagePack()
        {
            var pack = new MessagePack("We are currently experiencing network issues. Please try again later.")
            {
                Mappings = new Dictionary<string, string>
                {
                    ["ALC001"] = "Invalid Request",
                    ["ALC002"] = "No state was found.",
                    ["ALC003"] = "No local government was found.",
                    ["ALC004"] = "No request body was found.",


                    ["ALC999"] = "Opps, something went wrong. This is on us, please try again",
                },

            };

            return pack;
        }
    }
}
