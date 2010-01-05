#region License
/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Classless.Hasher - .NET Binary Encoding Library.
 *
 * The Initial Developer of the Original Code is Classless.net.
 * Portions created by the Initial Developer are Copyright (C) 2010 the Initial
 * Developer. All Rights Reserved.
 *
 * Contributor(s):
 *		Jason Simeone (jay@classless.net)
 * 
 * ***** END LICENSE BLOCK ***** */
#endregion

using System;
using NUnit.Framework;

namespace Classless.Encoder.Tests {
	[TestFixture]
	class EncoderTests {
		[Test]
		public void CreateDefaultTest() {
			Encoder encoder = Encoder.Create();
			Assert.IsNotNull(encoder);
			Assert.IsInstanceOf(typeof(Classless.Encoder.Base32Encoder), encoder);
		}

		[Test, TestCaseSource(typeof(Common), "EncoderNames")]
		public void CreateTest(string name, Type expectedType) {
			Assert.IsInstanceOf(expectedType, Encoder.Create(name));
		}

		[Test]
		public void CreateBadCastTest() {
			Assert.IsNull(Encoder.Create("Classless.Encoder.Base32Decoder"));
		}


		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void EncodeStreamBadTest() {
			Encoder encoder = Encoder.Create();
			encoder.Encode((System.IO.Stream)null);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void EncodeStreamBad2Test() {
			Encoder encoder = Encoder.Create();
			encoder.Encode((System.IO.Stream)null, 10);
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void EncodeStreamBadBufferTest() {
			Encoder encoder = Encoder.Create();
			encoder.Encode(new System.IO.MemoryStream(), -10);
		}
	}
}
