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
	class Base32HexDecoderTests {
		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void AlphabetNullTest() {
			Base32HexDecoder decoder = new Base32HexDecoder();
			decoder.Alphabet = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void AlphabetShortTest() {
			Base32HexDecoder decoder = new Base32HexDecoder();
			decoder.Alphabet = new char[1];
		}


		[Test]
		public void PaddingTest() {
			Base32HexDecoder decoder = new Base32HexDecoder();
			char testPadding = '?';
			decoder.Padding = testPadding;
			Assert.AreEqual(testPadding, decoder.Padding);
		}


		[Test]
		public void ConstructorDefaultTest() {
			Base32HexEncoder decoder = new Base32HexEncoder();
			Common.AreEqual(Base32HexEncoder.StandardAlphabet, decoder.Alphabet);
			Assert.AreEqual(Base32HexEncoder.StandardPadding, decoder.Padding);
		}


		[Test]
		public void IsCaseSensitiveTest() {
			Base32HexDecoder decoder = new Base32HexDecoder();
			Assert.IsFalse(decoder.IsCaseSensitive);
		}


		[Test]
		public void GetEncoderTest() {
			Base32HexDecoder decoder = new Base32HexDecoder();
			Encoder encoder = decoder.GetEncoder();
			Assert.IsNotNull(encoder);
			Assert.IsInstanceOf(typeof(Base32HexEncoder), encoder);
		}


		[Test, ExpectedException(typeof(ArgumentException))]
		public void InvalidCharacterTest() {
			Base32HexDecoder decoder = new Base32HexDecoder();
			decoder.Decode("ABCDE!");
		}
	}
}
