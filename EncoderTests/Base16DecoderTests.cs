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
	class Base16DecoderTests {
		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void AlphabetNullTest() {
			Base16Decoder decoder = new Base16Decoder();
			decoder.Alphabet = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void AlphabetShortTest() {
			Base16Decoder decoder = new Base16Decoder();
			decoder.Alphabet = new char[1];
		}


		[Test]
		public void ConstructorDefaultTest() {
			Base16Decoder decoder = new Base16Decoder();
			Common.AreEqual(Base16Encoder.StandardAlphabet, decoder.Alphabet);
		}

		[Test]
		public void ConstructorAlphabetTest() {
			char[] testAlphabet = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H' };
			Base16Decoder decoder = new Base16Decoder(testAlphabet);
			Common.AreEqual(testAlphabet, decoder.Alphabet);
		}


		[Test]
		public void IsCaseSensitiveTest() {
			Base16Decoder decoder = new Base16Decoder();
			Assert.IsFalse(decoder.IsCaseSensitive);
		}


		[Test]
		public void GetEncoderTest() {
			Base16Decoder decoder = new Base16Decoder();
			Encoder encoder = decoder.GetEncoder();
			Assert.IsNotNull(encoder);
			Assert.IsInstanceOf(typeof(Base16Encoder), encoder);
		}


		[Test, ExpectedException(typeof(ArgumentException))]
		public void InvalidCharacterTest() {
			Base16Decoder decoder = new Base16Decoder();
			decoder.Decode("ABCDE!");
		}


		[Test, ExpectedException(typeof(InvalidOperationException))]
		public void MissingCharacterTest() {
			Base16Decoder decoder = new Base16Decoder();
			decoder.Decode("ABCDE");
		}
	}
}
