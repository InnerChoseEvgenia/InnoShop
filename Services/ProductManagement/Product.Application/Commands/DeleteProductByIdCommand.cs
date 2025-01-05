﻿namespace Product.Application.Commands
{
    public class DeleteProductByIdCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteProductByIdCommand(string id)
        {
            Id = id;
        }
    }
}
