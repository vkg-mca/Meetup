using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Meetup.Test
{


    public class AdvancedTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IMeetupClient _client;
        public AdvancedTest(ITestOutputHelper output)
        {
            _output = output;
            _client = new MeetupClient();
        }

        [Theory]
        [InlineData(6, "Advanced: End-To-End Rapid API Development-6", 20)]
        [InlineData(7, "Advanced: End-To-End Rapid API Development-7", 30)]
        public async Task CreateMeetup_1(int id, string topic, int participantCount)
        {
            MeetupDetail meetup = new MeetupDetail()
            {
                Id = id,
                Topic = topic,
                ParticipantsCount = participantCount,
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
        [ClassData(typeof(MeetupTestData))]
        public async Task CreateMeetup_2(int id, string topic, int count, DateTime date)
        {
            MeetupDetail meetup = new MeetupDetail()
            {
                Id = id,
                Topic = topic,
                ParticipantsCount = count,
                Date = date
            };
            var result = await _client.ApiMeetupDetailsPostAsync(meetup);
            Assert.False(null == result);
            Assert.True(result.Id == meetup.Id);
            Assert.True(result.Topic == meetup.Topic);
            Assert.True(result.ParticipantsCount == meetup.ParticipantsCount);
            _output.WriteLine($"Id={result.Id},Date={result.Date},Count={result.ParticipantsCount},Topic={result.Topic}");
            Assert.False(null == result, "Result cannot be null");
        }

        [Theory]
        [InlineData(6, "Advanced: End-To-End Rapid API Development-8")]
        [InlineData(7, "Advanced: End-To-End Rapid API Development-9")]
        public async Task UpdateMeetup_3(int id, string topic)
        {
            MeetupDetail meetup = new MeetupDetail()
            {
                Id = id,
                Topic = topic,
                Date = DateTime.Now,
                ParticipantsCount = 40
            };
            await _client.ApiMeetupDetailsByIdPutAsync(id, meetup);
        }

    }

    public class MeetupTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 10, "Advanced: End-To-End Rapid API Development - 1", 10, DateTime.Now };
            yield return new object[] { 11, "Advanced: End-To-End Rapid API Development - 1", 20, DateTime.Now };
            yield return new object[] { 12, "Advanced: End-To-End Rapid API Development - 1", 30, DateTime.Now };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    }
}
