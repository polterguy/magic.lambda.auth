﻿/*
 * Magic, Copyright(c) Thomas Hansen 2019 - thomas@gaiasoul.com
 * Licensed as Affero GPL unless an explicitly proprietary license has been obtained.
 */

using System;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;
using magic.lambda.auth.helpers;
using magic.lambda.auth.contracts;

namespace magic.lambda.auth
{
    /// <summary>
    /// [auth.verify-ticket] slot, for verifying that a user is authenticated, and optionally belongs to
    /// one of the roles supplied as a comma separated list of values.
    /// </summary>
	[Slot(Name = "auth.verify-ticket")]
	public class VerifyTicket : ISlot
	{
        readonly ITicketProvider _ticketProvider;

        /// <summary>
        /// Creates a new instance of class.
        /// </summary>
        /// <param name="ticketProvider">Ticket provider, necessary to retrieve the authenticated user.</param>
        public VerifyTicket(ITicketProvider ticketProvider)
		{
            _ticketProvider = ticketProvider ?? throw new ArgumentNullException(nameof(ticketProvider));
        }

        /// <summary>
        /// Implementation of slot.
        /// </summary>
        /// <param name="signaler">Signaler used to raise the signal.</param>
        /// <param name="input">Arguments to signal.</param>
        public void Signal(ISignaler signaler, Node input)
		{
            TicketFactory.VerifyTicket(_ticketProvider, input.GetEx<string>());
            input.Value = true;
		}
    }
}
