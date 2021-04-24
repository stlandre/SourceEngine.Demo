using SourceEngine.Demo.Parser.BitStream;

namespace SourceEngine.Demo.Parser.Messages.Fast.Net
{
    /// <summary>
    /// Parser for the <c>CSVCMsg_ServerInfo</c> network message.
    /// </summary>
    public struct ServerInfo
    {
        /// <summary>
        /// Parses a <c>CSVCMsg_ServerInfo</c> network message from
        /// <paramref name="bitstream"/> and raises a <see cref="DemoParser.ServerInfo"/>
        /// event in the given <paramref name="parser"/>.
        /// </summary>
        /// <param name="bitstream">The stream to read the message data from.</param>
        /// <param name="parser">The <see cref="DemoParser"/> in which to raise the event.</param>
        public void Parse(IBitStream bitstream, DemoParser parser)
        {
            var e = new ServerInfoEventArgs();

            while (!bitstream.ChunkFinished)
            {
                var desc = bitstream.ReadProtobufVarInt();
                var wireType = desc & 7;
                var fieldnum = desc >> 3;

                switch (fieldnum)
                {
                    case 1:
                        if (wireType == 0)
                            e.Protocol = bitstream.ReadProtobufVarInt();

                        break;
                    case 2:
                        if (wireType == 0)
                            e.ServerCount = bitstream.ReadProtobufVarInt();

                        break;
                    case 3:
                        if (wireType == 0)
                            e.IsDedicated = bitstream.ReadProtobufVarInt() != 0;

                        break;
                    case 4:
                        if (wireType == 0)
                            e.IsOfficialValveServer = bitstream.ReadProtobufVarInt() != 0;

                        break;
                    case 5:
                        if (wireType == 0)
                            e.IsHltv = bitstream.ReadProtobufVarInt() != 0;

                        break;
                    case 6:
                        if (wireType == 0)
                            e.IsReplay = bitstream.ReadProtobufVarInt() != 0;

                        break;
                    case 7:
                        if (wireType == 0)
                            e.OperatingSystem = bitstream.ReadProtobufVarInt();

                        break;
                    case 8:
                        if (wireType == 5)
                            e.MapCrc = bitstream.ReadInt(32);

                        break;
                    case 9:
                        if (wireType == 5)
                            e.ClientCrc = bitstream.ReadInt(32);

                        break;
                    case 10:
                        if (wireType == 5)
                            e.StringTableCrc = bitstream.ReadInt(32);

                        break;
                    case 11:
                        if (wireType == 0)
                            e.MaxClients = bitstream.ReadProtobufVarInt();

                        break;
                    case 12:
                        if (wireType == 0)
                            e.MaxClasses = bitstream.ReadProtobufVarInt();

                        break;
                    case 13:
                        if (wireType == 0)
                            e.PlayerSlot = bitstream.ReadProtobufVarInt();

                        break;
                    case 14:
                        if (wireType == 5)
                            e.TickInterval = bitstream.ReadFloat();

                        break;
                    case 15:
                        if (wireType == 2)
                            e.GameDir = bitstream.ReadProtobufString();

                        break;
                    case 16:
                        if (wireType == 2)
                            e.MapName = bitstream.ReadProtobufString();

                        break;
                    case 17:
                        if (wireType == 2)
                            e.MapGroupName = bitstream.ReadProtobufString();

                        break;
                    case 18:
                        if (wireType == 2)
                            e.SkyName = bitstream.ReadProtobufString();

                        break;
                    case 19:
                        if (wireType == 2)
                            e.HostName = bitstream.ReadProtobufString();

                        break;
                    case 20:
                        if (wireType == 0)
                            e.PublicIp = (uint)bitstream.ReadProtobufVarInt();

                        break;
                    case 21:
                        if (wireType == 0)
                            e.IsRedirectingToProxyRelay = bitstream.ReadProtobufVarInt() != 0;

                        break;
                    case 22:
                        // TODO: there don't seem to be any facilities for reading uint64
                        break;
                }
            }

            parser.RaiseServerInfo(e);
        }
    }
}