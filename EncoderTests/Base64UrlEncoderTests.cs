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
	class Base64UrlEncoderTests {
		[Test]
		public void StandardAlphabetTest() {
			char[] testAlphabet = new char[] { 'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7','8','9','-','_' };
			Common.AreEqual(testAlphabet, Base64UrlEncoder.StandardAlphabet);
		}

		[Test]
		public void StandardPaddingTest() {
			char testPadding = '=';
			Assert.AreEqual(testPadding, Base64UrlEncoder.StandardPadding);
		}


		[Test]
		public void AlphabetTest() {
			char[] testAlphabet = new char[] {
				'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6',
				'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m','7','8','9','0','[',']'
			};
			Base64UrlEncoder encoder = new Base64UrlEncoder();
			encoder.Alphabet = testAlphabet;
			Common.AreEqual(testAlphabet, encoder.Alphabet);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void AlphabetNullTest() {
			Base64UrlEncoder encoder = new Base64UrlEncoder();
			encoder.Alphabet = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void AlphabetShortTest() {
			Base64UrlEncoder encoder = new Base64UrlEncoder();
			encoder.Alphabet = new char[1];
		}


		[Test]
		public void PaddingTest() {
			Base64UrlEncoder encoder = new Base64UrlEncoder();
			char testPadding = '?';
			encoder.Padding = testPadding;
			Assert.AreEqual(testPadding, encoder.Padding);
		}


		[Test]
		public void ConstructorDefaultTest() {
			Base64UrlEncoder encoder = new Base64UrlEncoder();
			Common.AreEqual(Base64UrlEncoder.StandardAlphabet, encoder.Alphabet);
			Assert.AreEqual(Base64UrlEncoder.StandardPadding, encoder.Padding);
		}


		[Test]
		public void GetDecoderTest() {
			Base64UrlEncoder encoder = new Base64UrlEncoder();
			Decoder decoder = encoder.GetDecoder();
			Assert.IsNotNull(decoder);
			Assert.IsInstanceOf(typeof(Base64UrlDecoder), decoder);
		}
	}
}
