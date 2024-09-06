using System;
using Beam.Models;
using Beam.Util;

namespace Beam.Extensions
{
    internal static class BeamSessionExtensions
    {
        internal static bool IsActive(this BeamSession beamSession)
        {
            // add some time to take into account processing time
            var nowWithMargin = DateTimeOffset.Now.AddSeconds(30);
            return beamSession != null && beamSession.StartTime <= nowWithMargin && beamSession.EndTime > nowWithMargin;
        }

        internal static bool IsOwnedBy(this BeamSession beamSession, KeyPair keyPair)
        {
            return string.Equals(beamSession.SessionAddress, keyPair.Account.Address,
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}