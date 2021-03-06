﻿//
// Program.cs
//
// Author:
//       Matt Ward <matt.ward@xamarin.com>
//
// Copyright (c) 2016 Xamarin Inc. (http://xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using Microsoft.PowerShell.EditorServices.Protocol.Client;
using Microsoft.PowerShell.EditorServices.Protocol.MessageProtocol.Channel;
using System.Threading.Tasks;

namespace PowerShellEditorServicesTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try {
				var program = new MainClass ();
				program.Run ();
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
		}

		void Run ()
		{
			var session = new PowerShellSession ();
			session.Started += Session_Started;
			session.Start ();

			System.Threading.Thread.Sleep (1000);
			Console.WriteLine ("Press a key to quit.");
			Console.ReadKey ();

			session.Stop ();
		}

		void Session_Started (object sender, EventArgs e)
		{
			try {
				var session = (PowerShellSession)sender;
				StartLanguageClient (session.SessionDetails).Wait ();
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
		}

		Task StartLanguageClient (SessionDetailsMessage sessionDetails)
		{
			var channel = new TcpSocketClientChannel (sessionDetails.LanguageServicePort);
			var client = new LanguageServiceClient (channel);
			return client.Start ();
		}
	}
}
