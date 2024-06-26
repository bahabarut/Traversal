﻿using MediatR;

namespace TraversalCore.CQRS.Commands.GuideCommands
{
    public class CreateGuideCommand : IRequest
    {
        public string Name { get; set; }
        public string Desription { get; set; }
    }
}
