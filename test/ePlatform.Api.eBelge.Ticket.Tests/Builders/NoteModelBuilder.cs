using System.Collections.Generic;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.Tests.Builders.Base;

namespace ePlatform.Api.eBelge.Ticket.Tests.Builders
{
    public class NoteModelBuilder : BuilderBase<NoteModel, NoteModelBuilder>
    {
        public override NoteModelBuilder CreateWithDefaultValues()
        {
            _concreteObject = new NoteModel {Note = "test note - 1"};

            return this;
        }
    }
}
