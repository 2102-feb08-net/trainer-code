using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApp.WebUI.Models;

namespace EmailApp.WebUI.Data
{
    public class MessageRepository
    {
        private readonly List<Message> _inbox = new()
        {
            new()
            {
                Id = 1,
                From = "fred@fred.com",
                Date = DateTimeOffset.Parse("Mon, 01 Mar 2021 12:58:58 -0700"),
                Subject = "qc",
                Body = "Aenean elit massa, eleifend id feugiat a, semper in massa. Praesent ex lectus, vehicula eget mi ut, dictum commodo tortor. Sed congue leo ac mollis hendrerit. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Pellentesque ut magna non sapien efficitur ullamcorper. Donec elementum, purus aliquet facilisis auctor, massa justo finibus leo, a feugiat purus tortor vitae ante. Suspendisse ipsum nibh, tincidunt congue mattis ut, tristique in felis."
            },
            new()
            {
                Id = 2,
                From = "kevin@kevin.com",
                Date = DateTimeOffset.Parse("Mon, 01 Mar 2021 13:00:10 -0700"),
                Subject = "RE: qc",
                Body = "Donec egestas lorem viverra augue placerat interdum. Nulla id mollis purus. Quisque eget libero ultricies est tincidunt tempor. Integer lobortis sapien et pellentesque tincidunt. Nulla euismod pulvinar lorem sed pellentesque. Ut sit amet quam non elit pharetra cursus. In hac habitasse platea dictumst. Proin accumsan a justo ac molestie. Phasellus eu metus neque. Donec vel sollicitudin libero. Donec sed leo leo."
            },
        };
        private readonly List<Message> _outbox = new();

        public IEnumerable<Message> List()
        {
            return _inbox;
        }

        public Message Get(int id)
        {
            return _inbox.FirstOrDefault(x => x.Id == id);
        }

        public void Create(Message message)
        {
            _outbox.Add(message);
        }
    }
}
