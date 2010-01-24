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
	class ZBase32EncoderTests {
		[Test]
		public void StandardAlphabetTest() {
			char[] testAlphabet = new char[] { 'y','b','n','d','r','f','g','8','e','j','k','m','c','p','q','x','o','t','1','u','w','i','s','z','a','3','4','5','h','7','6','9' };
			Common.AreEqual(testAlphabet, ZBase32Encoder.StandardAlphabet);
		}

		[Test]
		public void StandardPaddingTest() {
			char testPadding = '=';
			Assert.AreEqual(testPadding, ZBase32Encoder.StandardPadding);
		}


		[Test]
		public void AlphabetTest() {
			char[] testAlphabet = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			ZBase32Encoder encoder = new ZBase32Encoder();
			encoder.Alphabet = testAlphabet;
			Common.AreEqual(testAlphabet, encoder.Alphabet);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void AlphabetNullTest() {
			ZBase32Encoder encoder = new ZBase32Encoder();
			encoder.Alphabet = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void AlphabetShortTest() {
			ZBase32Encoder encoder = new ZBase32Encoder();
			encoder.Alphabet = new char[1];
		}


		[Test]
		public void PaddingTest() {
			ZBase32Encoder encoder = new ZBase32Encoder();
			char testPadding = '?';
			encoder.Padding = testPadding;
			Assert.AreEqual(testPadding, encoder.Padding);
		}


		[Test]
		public void ConstructorDefaultTest() {
			ZBase32Encoder encoder = new ZBase32Encoder();
			Common.AreEqual(ZBase32Encoder.StandardAlphabet, encoder.Alphabet);
			Assert.AreEqual(ZBase32Encoder.StandardPadding, encoder.Padding);
		}


		[Test]
		public void GetDecoderTest() {
			ZBase32Encoder encoder = new ZBase32Encoder();
			Decoder decoder = encoder.GetDecoder();
			Assert.IsNotNull(decoder);
			Assert.IsInstanceOf(typeof(ZBase32Decoder), decoder);
		}
	}
}
