//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.PowerShell.EditorServices.Protocol.MessageProtocol;

namespace Microsoft.PowerShell.EditorServices.Protocol.DebugAdapter
{
    public class ExitedEvent
    {
        public static readonly
            EventType<ExitedEventBody> Type =
            EventType<ExitedEventBody>.Create("exited");
    }

    public class ExitedEventBody
    {
        public int ExitCode { get; set; }
    }
}

