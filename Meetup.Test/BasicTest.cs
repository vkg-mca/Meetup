using System;
using System.Net;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Meetup.Test
{
    public class BasicTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IMeetupClient _client;
        public BasicTest(ITestOutputHelper output)
        {
            _output = output;
            _client = new MeetupClient();
        }

        [Fact]
        public async Task CreateMeetup()
        {
            //Create
            MeetupDetail expected = new MeetupDetail()
            {
                Id = 1,
                Topic = $"End-To-End Rapid API Development-1",
                ParticipantsCount = 25,
                Date = DateTime.Now
            };
            await _client.ApiMeetupDetailsPostAsync(expected);
            //Get
            var actual = await _client.ApiMeetupDetailsByIdGetAsync(1);
            //Assert
            Assert.False(null == actual);
            Assert.True(actual.Id == expected.Id);
            Assert.True(actual.Topic == expected.Topic);
            Assert.True(actual.ParticipantsCount == expected.ParticipantsCount);
            _output.WriteLine($"Id={actual.Id},Date={actual.Date},Count={actual.ParticipantsCount},Topic={actual.Topic}");

        }

        [Theory]
        [InlineData(2, "End-To-End Rapid API Development-1", 50)]
        public async Task UpdateMeetup(int id, string topic, int participantCount)
        {
            //Create
            MeetupDetail created = new MeetupDetail()
            {
                Id = id,
                Topic = topic,
                ParticipantsCount = participantCount,
                Date = DateTime.Now
            };
            await _client.ApiMeetupDetailsPostAsync(created);
            //Update
            MeetupDetail expected = new MeetupDetail()
            {
                Id = id,
                Topic = topic + ": Updated",
                ParticipantsCount = participantCount * 2,
                Date = DateTime.Now
            };
            await _client.ApiMeetupDetailsByIdPutAsync(id, expected);
            //Get
            var actual = await _client.ApiMeetupDetailsByIdGetAsync(id);
            //Assert
            Assert.False(null == actual);
            Assert.True(actual.Id == expected.Id);
            Assert.True(actual.Topic == expected.Topic);
            Assert.True(actual.ParticipantsCount == expected.ParticipantsCount);
            _output.WriteLine($"Id={actual.Id},Date={actual.Date},Count={actual.ParticipantsCount},Topic={actual.Topic}");

        }


        [Theory]
        [InlineData(3)]
        public async Task DeleteMeetup(int id)
        {
            //Create
            MeetupDetail created = new MeetupDetail()
            {
                Id = id,
                Topic = "End-To-End Rapid API Development",
                ParticipantsCount = 10,
                Date = DateTime.Now
            };
            await _client.ApiMeetupDetailsPostAsync(created);
            //Get
            var retrived = await _client.ApiMeetupDetailsByIdGetAsync(id);
            //Delete
            var deleted = await _client.ApiMeetupDetailsByIdDeleteAsync(id);
            //Get
            MeetupException actual = await Assert.ThrowsAsync<MeetupException<ProblemDetails>>(async ()
                 => await _client.ApiMeetupDetailsByIdDeleteAsync(id));
            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, actual.StatusCode);
        }
    }
}
