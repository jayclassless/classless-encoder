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
	class UUEncoderTests {
		[Test]
		public void StandardAlphabetTest() {
			char[] testAlphabet = new char[] { '`','!','"','#','$','%','&','\'','(',')','*','+',',','-','.','/','0','1','2','3','4','5','6','7','8','9',':',';','<','=','>','?','@','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','[','\\',']','^','_' };
			Common.AreEqual(testAlphabet, UUEncoder.StandardAlphabet);
		}


		[Test]
		public void AlphabetTest() {
			char[] testAlphabet = new char[] {
				'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6',
				'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m','7','8','9','0','[',']'
			};
			UUEncoder encoder = new UUEncoder();
			encoder.Alphabet = testAlphabet;
			Common.AreEqual(testAlphabet, encoder.Alphabet);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void AlphabetNullTest() {
			UUEncoder encoder = new UUEncoder();
			encoder.Alphabet = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void AlphabetShortTest() {
			UUEncoder encoder = new UUEncoder();
			encoder.Alphabet = new char[1];
		}


		[Test]
		public void ConstructorDefaultTest() {
			UUEncoder encoder = new UUEncoder();
			Common.AreEqual(UUEncoder.StandardAlphabet, encoder.Alphabet);
		}

		[Test]
		public void ConstructorAlphabetTest() {
			char[] testAlphabet = new char[] {
				'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6',
				'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m','7','8','9','0','[',']'
			};
			UUEncoder encoder = new UUEncoder(testAlphabet);
			Common.AreEqual(testAlphabet, encoder.Alphabet);
		}


		[Test, ExpectedException(typeof(NotImplementedException))]
		public void GetDecoderTest() {
			UUEncoder encoder = new UUEncoder();
			Decoder decoder = encoder.GetDecoder();
			//Assert.IsNotNull(decoder);
			//Assert.IsInstanceOf(typeof(UUDecoder), decoder);
		}


		[Test]
		public void GetOutputEncodingTest() {
			UUEncoder encoder = new UUEncoder();
			System.Text.Encoding encoding = encoder.GetOutputEncoding();
			Assert.IsNotNull(encoding);
			Assert.AreEqual(System.Text.Encoding.ASCII, encoding);
		}
	}
}
