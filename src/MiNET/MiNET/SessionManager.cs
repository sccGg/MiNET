﻿#region LICENSE

// The contents of this file are subject to the Common Public Attribution
// License Version 1.0. (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
// https://github.com/NiclasOlofsson/MiNET/blob/master/LICENSE. 
// The License is based on the Mozilla Public License Version 1.1, but Sections 14 
// and 15 have been added to cover use of software over a computer network and 
// provide for limited attribution for the Original Developer. In addition, Exhibit A has 
// been modified to be consistent with Exhibit B.
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// The Original Code is MiNET.
// 
// The Original Developer is the Initial Developer.  The Initial Developer of
// the Original Code is Niclas Olofsson.
// 
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2018 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using System.Collections.Concurrent;
using System.Collections.Generic;
using MiNET.Net;
using MiNET.Net.RakNet;
using MiNET.Utils;

namespace MiNET
{
	public class SessionManager
	{
		private ConcurrentDictionary<UUID, Session> _sessions = new ConcurrentDictionary<UUID, Session>();

		public virtual Session FindSession(Player.Player player)
		{
			Session session;
			_sessions.TryGetValue(player.ClientUuid, out session);

			return session;
		}

		public virtual Session CreateSession(Player.Player player)
		{
			_sessions.TryAdd(player.ClientUuid, new Session(player));

			return FindSession(player);
		}

		public virtual void SaveSession(Session session)
		{
		}

		public virtual void RemoveSession(Session session)
		{
			if (session.Player == null) return;
			if (session.Player.ClientUuid == null) return;

			_sessions.TryRemove(session.Player.ClientUuid, out session);
		}
	}

	public class Session : Dictionary<string, object>
	{
		public Player.Player Player { get; set; }

		public Session(Player.Player player)
		{
			Player = player;
		}
	}
}