using System;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Meetup.Test
{
    public class SimpleTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IClient _client;
        public SimpleTest(ITestOutputHelper output)
        {
            _output = output;
            _client = new Client("http://localhost:3987");
        }

        [Fact]
        public async Task CreateMeetup_1()
        {
            MeetupDetail meetup = new MeetupDetail()
            {
                Id = 1,
                Topic = $"End-To-End Rapid API Development-1",
                ParticipantsCount = 100,
                Date = DateTime.Now
            };

            var result = await _client.ApiMeetupDetailsPostAsync(meetup);
            Assert.False(null == result);
            Assert.True(result.Id == meetup.Id);
            Assert.True(result.Topic == meetup.Topic);
            Assert.True(result.ParticipantsCount == meetup.ParticipantsCount);
            _output.WriteLine($"Id={result.Id},Date={result.Date},Count={result.ParticipantsCount},Topic={result.Topic}");

        }

        [Theory]
        [InlineData(1, "End-To-End Rapid API Development-1", 100)]
        public async Task GetMeetupById_2(int id, string topic, int participantCount)
        {
            var meetup = await _client.ApiMeetupDetailsByIdGetAsync(1);
            Assert.False(null == meetup);
            Assert.True(id == meetup.Id);
            Assert.True(topic == meetup.Topic);
            Assert.True(participantCount == meetup.ParticipantsCount);

            _output.WriteLine($"Id={meetup.Id},Date={meetup.Date},Count={meetup.ParticipantsCount},Topic={meetup.Topic}");
        }

        [Fact]
        public async Task CreateMeetup_3()
        {
            MeetupDetail meetup = new MeetupDetail()
            {
                Id = 2,
                Topic = $"End-To-End Rapid API Development-2",
                ParticipantsCount = 100,
                Date = DateTime.Now
            };

            var result = await _client.ApiMeetupDetailsPostAsync(meetup);
            Assert.False(null == result);
            Assert.True(result.Id == meetup.Id);
            Assert.True(result.Topic == meetup.Topic);
            Assert.True(result.ParticipantsCount == meetup.ParticipantsCount);
            _output.WriteLine($"Id={result.Id},Date={result.Date},Count={result.ParticipantsCount},Topic={result.Topic}");

        }

        [Fact]
        public async Task GetAllMeetups_4()
        {
            var meetups = await _client.ApiMeetupDetailsGetAsync();
            Assert.False(null == meetups);
            foreach (var meetup in meetups)
            {
                _output.WriteLine($"Id={meetup.Id},Date={meetup.Date},Count={meetup.ParticipantsCount},Topic={meetup.Topic}");
            }
        }

        [Fact]
        public async Task UpdateMeetup_5()
        {
            int id = 1;
            MeetupDetail meetup = new MeetupDetail()
            {
                Id = 1,
                Topic = $"End-To-End Rapid API Development-1: Updated",
                ParticipantsCount = 100,
                Date = DateTime.Now
            };
            await _client.ApiMeetupDetailsByIdPutAsync(id, meetup);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task DeleteMeetup_6(int id)
        {
            var meetups = await _client.ApiMeetupDetailsByIdDeleteAsync(id);
            Assert.False(null == meetups);
        }

    }
}
