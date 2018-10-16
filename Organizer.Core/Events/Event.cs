using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organizer.Events
{
    [Table("AppEvents")]
    public class Event : FullAuditedEntity<Guid>
    {
        public const int MaxTitleLength = 128;
        public const int MaxDescriptionLength = 2048;

        [Required]
        [StringLength(MaxTitleLength)]
        public virtual string Title { get; protected set; }

        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; protected set; }

        [Required]
        public virtual DateTime Start { get; protected set; }

        public virtual DateTime End { get; protected set; }

        [Required]
        public virtual bool AllDay { get; protected set; }


        [ForeignKey("EventListId")]
        public virtual EventList EventList { get; protected set; }
        public virtual Guid EventListId { get; protected set; }


        public string Color => EventList?.Color;

        public string BorderColor => EventList.Owner ? null : "#000000";

        protected Event() { }

        public static Event Create(string title, DateTime startAt, bool isFullDay, Guid eventListId, string description = null, DateTime? endAt = null)
        {
            var @event = new Event
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                EventListId = eventListId,
                AllDay = isFullDay
            };

            @event.SetDates(startAt, endAt);

            return @event;
        }

        public void Update(string title = null, string description = null, DateTime? startAt = null, DateTime? endAt = null,
            bool? isFullDay = null, Guid? listId = null)
        {
            Title = title ?? Title;
            Start = startAt ?? Start;
            AllDay = isFullDay ?? AllDay;
            Description = description ?? Description;
            End = endAt ?? End;
            EventListId = listId ?? EventListId;
        }

        public bool IsInPast()
        {
            return Start < Clock.Now;
        }


        public void ChangeDates(DateTime startAt, DateTime? endAt = null)
        {
            if (startAt == Start && endAt == End)
                return;

            SetDates(startAt, endAt);

            // EventBus.Trigger(new EventDateChangedEvent(this));
        }

        private void SetDates(DateTime startAt, DateTime? endAt = null)
        {
            if (startAt.Date.AddDays(1) <= Clock.Now.Date)
                throw new UserFriendlyException("Can`t set an event's date in the past!");
            if (!endAt.HasValue)
                endAt = startAt;
            else if (endAt < startAt)
                throw new UserFriendlyException("End date can not be before start date!");

            Start = startAt;
            End = endAt.Value;

            //  EventBus.Trigger(new EventDateChangedEvent(this));
        }
    }
}
