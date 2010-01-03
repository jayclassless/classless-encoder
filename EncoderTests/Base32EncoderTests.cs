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
	class Base32EncoderTests {
		[Test]
		public void StandardAlphabetTest() {
			char[] testAlphabet = new char[] { 'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','2','3','4','5','6','7' };
			Base32Encoder encoder = new Base32Encoder();
			Common.AreEqual(testAlphabet, encoder.Alphabet);
		}

		[Test]
		public void StandardPaddingTest() {
			char testPadding = '=';
			Base32Encoder encoder = new Base32Encoder();
			Assert.AreEqual(testPadding, encoder.Padding);
		}


		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void AlphabetNullTest() {
			Base32Encoder encoder = new Base32Encoder();
			encoder.Alphabet = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void AlphabetShortTest() {
			Base32Encoder encoder = new Base32Encoder();
			encoder.Alphabet = new char[1];
		}


		[Test]
		public void PaddingTest() {
			Base32Encoder encoder = new Base32Encoder();
			char testPadding = '?';
			encoder.Padding = testPadding;
			Assert.AreEqual(testPadding, encoder.Padding);
		}


		[Test]
		public void ConstructorDefaultTest() {
			Base32Encoder encoder = new Base32Encoder();
			Common.AreEqual(Base32Encoder.StandardAlphabet, encoder.Alphabet);
			Assert.AreEqual(Base32Encoder.StandardPadding, encoder.Padding);
		}

		[Test]
		public void ConstructorAlphabetTest() {
			char[] testAlphabet = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			Base32Encoder encoder = new Base32Encoder(testAlphabet);
			Common.AreEqual(testAlphabet, encoder.Alphabet);
			Assert.AreEqual(Base32Encoder.StandardPadding, encoder.Padding);
		}

		[Test]
		public void ConstructorAlphabetPaddingTest() {
			char[] testAlphabet = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			char testPadding = '?';
			Base32Encoder encoder = new Base32Encoder(testAlphabet, testPadding);
			Common.AreEqual(testAlphabet, encoder.Alphabet);
			Assert.AreEqual(testPadding, encoder.Padding);
		}


		[Test]
		public void GetDecoderTest() {
			Base32Encoder encoder = new Base32Encoder();
			Decoder decoder = encoder.GetDecoder();
			Assert.IsNotNull(decoder);
			Assert.IsInstanceOf(typeof(Base32Decoder), decoder);
		}
	}
}
