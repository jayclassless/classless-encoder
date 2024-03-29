﻿#region License
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
 * The Original Code is Classless.Encoder - .NET Binary Encoding Library.
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
	class DecoderTests {
		[Test]
		public void CreateDefaultTest() {
			Decoder decoder = Decoder.Create();
			Assert.IsNotNull(decoder);
			Assert.IsInstanceOf(typeof(Classless.Encoder.Base32Decoder), decoder);
		}

		[Test, TestCaseSource(typeof(Common), "DecoderNames")]
		public void CreateTest(string name, Type expectedType) {
			Assert.IsInstanceOf(expectedType, Decoder.Create(name));
		}

		[Test]
		public void CreateBadCastTest() {
			Assert.IsNull(Decoder.Create("Classless.Encoder.Base32Encoder"));
		}


		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void DecodeStreamBadTest() {
			Decoder decoder = Decoder.Create();
			decoder.Decode((System.IO.Stream)null);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void DecodeStreamBad2Test() {
			Decoder decoder = Decoder.Create();
			decoder.Decode((System.IO.Stream)null, 10);
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void DecodeStreamBadBufferTest() {
			Decoder decoder = Decoder.Create();
			decoder.Decode(new System.IO.MemoryStream(), -10);
		}
	}
}
