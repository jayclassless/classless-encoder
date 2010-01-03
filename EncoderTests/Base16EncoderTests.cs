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
	class Base16EncoderTests {
		[Test]
		public void StandardAlphabetTest() {
			char[] testAlphabet = new char[] { '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F' };
			Common.AreEqual(testAlphabet, Base16Encoder.StandardAlphabet);
		}


		[Test]
		public void AlphabetTest() {
			char[] testAlphabet = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H' };
			Base16Encoder encoder = new Base16Encoder();
			encoder.Alphabet = testAlphabet;
			Common.AreEqual(testAlphabet, encoder.Alphabet);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void AlphabetNullTest() {
			Base16Encoder encoder = new Base16Encoder();
			encoder.Alphabet = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void AlphabetShortTest() {
			Base16Encoder encoder = new Base16Encoder();
			encoder.Alphabet = new char[1];
		}


		[Test]
		public void ConstructorDefaultTest() {
			Base16Encoder encoder = new Base16Encoder();
			Common.AreEqual(Base16Encoder.StandardAlphabet, encoder.Alphabet);
		}

		[Test]
		public void ConstructorAlphabetTest() {
			char[] testAlphabet = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H' };
			Base16Encoder encoder = new Base16Encoder(testAlphabet);
			Common.AreEqual(testAlphabet, encoder.Alphabet);
		}


		[Test]
		public void GetDecoderTest() {
			Base16Encoder encoder = new Base16Encoder();
			Decoder decoder = encoder.GetDecoder();
			Assert.IsNotNull(decoder);
			Assert.IsInstanceOf(typeof(Base16Decoder), decoder);
		}
	}
}
