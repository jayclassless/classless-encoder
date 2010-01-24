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
	class ZBase32DecoderTests {
		[Test]
		public void AlphabetTest() {
			char[] testAlphabet = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			ZBase32Decoder decoder = new ZBase32Decoder();
			decoder.Alphabet = testAlphabet;
			Common.AreEqual(testAlphabet, decoder.Alphabet);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void AlphabetNullTest() {
			ZBase32Decoder decoder = new ZBase32Decoder();
			decoder.Alphabet = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void AlphabetShortTest() {
			ZBase32Decoder decoder = new ZBase32Decoder();
			decoder.Alphabet = new char[1];
		}


		[Test]
		public void PaddingTest() {
			ZBase32Decoder decoder = new ZBase32Decoder();
			char testPadding = '?';
			decoder.Padding = testPadding;
			Assert.AreEqual(testPadding, decoder.Padding);
		}


		[Test]
		public void ConstructorDefaultTest() {
			ZBase32Decoder decoder = new ZBase32Decoder();
			Common.AreEqual(ZBase32Encoder.StandardAlphabet, decoder.Alphabet);
			Assert.AreEqual(ZBase32Encoder.StandardPadding, decoder.Padding);
		}


		[Test]
		public void IsCaseSensitiveTest() {
			ZBase32Decoder decoder = new ZBase32Decoder();
			Assert.IsTrue(decoder.IsCaseSensitive);
		}


		[Test]
		public void GetEncoderTest() {
			ZBase32Decoder decoder = new ZBase32Decoder();
			Encoder encoder = decoder.GetEncoder();
			Assert.IsNotNull(encoder);
			Assert.IsInstanceOf(typeof(ZBase32Encoder), encoder);
		}


		[Test, ExpectedException(typeof(ArgumentException))]
		public void InvalidCharacterTest() {
			ZBase32Decoder decoder = new ZBase32Decoder();
			decoder.Decode("ABCDE!");
		}
	}
}
