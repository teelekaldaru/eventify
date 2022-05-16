﻿using Eventify.Common.Classes.AutoMapper;
using Eventify.Common.Classes.Events;
using Eventify.Domain;

namespace Eventify.DAL.Events
{
    internal static class EventExtensions
    {
        public static Event ToEvent(this DbEvent dbEvent)
        {
            return MapperWrapper.Mapper.Map<Event>(dbEvent);
        }
    }
}
