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
	class Base64UrlDecoderTests {
		[Test]
		public void AlphabetTest() {
			char[] testAlphabet = new char[] {
				'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6',
				'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m','7','8','9','0','[',']'
			};
			Base64UrlDecoder decoder = new Base64UrlDecoder();
			decoder.Alphabet = testAlphabet;
			Common.AreEqual(testAlphabet, decoder.Alphabet);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void AlphabetNullTest() {
			Base64UrlDecoder decoder = new Base64UrlDecoder();
			decoder.Alphabet = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void AlphabetShortTest() {
			Base64UrlDecoder decoder = new Base64UrlDecoder();
			decoder.Alphabet = new char[1];
		}


		[Test]
		public void ConstructorDefaultTest() {
			Base64UrlDecoder decoder = new Base64UrlDecoder();
			Common.AreEqual(Base64UrlEncoder.StandardAlphabet, decoder.Alphabet);
			Assert.AreEqual(Base64UrlEncoder.StandardPadding, decoder.Padding);
		}


		[Test]
		public void IsCaseSensitiveTest() {
			Base64UrlDecoder decoder = new Base64UrlDecoder();
			Assert.IsTrue(decoder.IsCaseSensitive);
		}


		[Test]
		public void GetEncoderTest() {
			Base64UrlDecoder decoder = new Base64UrlDecoder();
			Encoder encoder = decoder.GetEncoder();
			Assert.IsNotNull(encoder);
			Assert.IsInstanceOf(typeof(Base64UrlEncoder), encoder);
		}


		[Test, ExpectedException(typeof(ArgumentException))]
		public void InvalidCharacterTest() {
			Base64UrlDecoder decoder = new Base64UrlDecoder();
			decoder.Decode("ABCDE!");
		}
	}
}
