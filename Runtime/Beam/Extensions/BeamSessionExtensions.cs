using System;
using Beam.Models;
using Beam.Util;

namespace Beam
{
    internal static class BeamSessionExtensions
    {
        internal static bool IsValidNow(this BeamSession beamSession)
        {
            var now = DateTimeOffset.Now;
            return beamSession != null && beamSession.StartTime <= now && beamSession.EndTime > now;
        }

        internal static bool IsOwnedBy(this BeamSession beamSession, KeyPair keyPair)
        {
            return string.Equals(beamSession.SessionAddress, keyPair.Account.Address,
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}