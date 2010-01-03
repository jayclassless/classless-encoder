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
	class Base64DecoderTests {
		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void AlphabetNullTest() {
			Base64Decoder decoder = new Base64Decoder();
			decoder.Alphabet = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void AlphabetShortTest() {
			Base64Decoder decoder = new Base64Decoder();
			decoder.Alphabet = new char[1];
		}


		[Test]
		public void PaddingTest() {
			Base64Decoder decoder = new Base64Decoder();
			char testPadding = '?';
			decoder.Padding = testPadding;
			Assert.AreEqual(testPadding, decoder.Padding);
		}


		[Test]
		public void ConstructorDefaultTest() {
			Base64Decoder decoder = new Base64Decoder();
			Common.AreEqual(Base64Encoder.StandardAlphabet, decoder.Alphabet);
			Assert.AreEqual(Base64Encoder.StandardPadding, decoder.Padding);
		}

		[Test]
		public void ConstructorAlphabetTest() {
			char[] testAlphabet = new char[] {
				'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6',
				'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m','7','8','9','0','[',']'
			};
			Base64Decoder decoder = new Base64Decoder(testAlphabet);
			Common.AreEqual(testAlphabet, decoder.Alphabet);
			Assert.AreEqual(Base64Encoder.StandardPadding, decoder.Padding);
		}

		[Test]
		public void ConstructorAlphabetPaddingTest() {
			char[] testAlphabet = new char[] {
				'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6',
				'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m','7','8','9','0','[',']'
			};
			char testPadding = '?';
			Base64Decoder decoder = new Base64Decoder(testAlphabet, testPadding);
			Common.AreEqual(testAlphabet, decoder.Alphabet);
			Assert.AreEqual(testPadding, decoder.Padding);
		}


		[Test]
		public void IsCaseSensitiveTest() {
			Base64Decoder decoder = new Base64Decoder();
			Assert.IsTrue(decoder.IsCaseSensitive);
		}


		[Test]
		public void GetEncoderTest() {
			Base64Decoder decoder = new Base64Decoder();
			Encoder encoder = decoder.GetEncoder();
			Assert.IsNotNull(encoder);
			Assert.IsInstanceOf(typeof(Base64Encoder), encoder);
		}


		[Test, ExpectedException(typeof(ArgumentException))]
		public void InvalidCharacterTest() {
			Base64Decoder decoder = new Base64Decoder();
			decoder.Decode("ABCDE!");
		}
	}
}
