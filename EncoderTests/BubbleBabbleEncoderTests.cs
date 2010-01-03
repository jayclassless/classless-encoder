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
	class BubbleBabbleEncoderTests {
		[Test]
		public void StandardVowelsTest() {
			char[] testVowels = new char[] { 'a','e','i','o','u','y' };
			Common.AreEqual(testVowels, BubbleBabbleEncoder.StandardVowels);
		}

		[Test]
		public void StandardConsonantsTest() {
			char[] testConsonants = new char[] { 'b','c','d','f','g','h','k','l','m','n','p','r','s','t','v','z','x' };
			Common.AreEqual(testConsonants, BubbleBabbleEncoder.StandardConsonants);
		}

		[Test]
		public void StandardDividerTest() {
			char testDivider = '-';
			Assert.AreEqual(testDivider, BubbleBabbleEncoder.StandardDivider);
		}

		[Test]
		public void StandardWrappingTest() {
			char testWrapping = 'x';
			Assert.AreEqual(testWrapping, BubbleBabbleEncoder.StandardWrapping);
		}


		[Test]
		public void VowelsTest() {
			char[] testVowels = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder();
			encoder.Vowels = testVowels;
			Common.AreEqual(testVowels, encoder.Vowels);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void VowelsNullTest() {
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder();
			encoder.Vowels = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void VowelsShortTest() {
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder();
			encoder.Vowels = new char[1];
		}


		[Test]
		public void ConsonantsTest() {
			char[] testConsonants = new char[] { 'M','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder();
			encoder.Consonants = testConsonants;
			Common.AreEqual(testConsonants, encoder.Consonants);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void ConsonantsNullTest() {
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder();
			encoder.Consonants = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void ConsonantsShortTest() {
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder();
			encoder.Consonants = new char[1];
		}


		[Test]
		public void DividerTest() {
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder();
			char testDivider = '?';
			encoder.Divider = testDivider;
			Assert.AreEqual(testDivider, encoder.Divider);
		}


		[Test]
		public void WrappingTest() {
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder();
			char testWrapping = '?';
			encoder.Wrapping = testWrapping;
			Assert.AreEqual(testWrapping, encoder.Wrapping);
		}


		[Test]
		public void ConstructorDefaultTest() {
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder();
			Common.AreEqual(BubbleBabbleEncoder.StandardVowels, encoder.Vowels);
			Common.AreEqual(BubbleBabbleEncoder.StandardConsonants, encoder.Consonants);
			Assert.AreEqual(BubbleBabbleEncoder.StandardDivider, encoder.Divider);
			Assert.AreEqual(BubbleBabbleEncoder.StandardWrapping, encoder.Wrapping);
		}

		[Test]
		public void ConstructorVowelsTest() {
			char[] testVowels = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder(testVowels);
			Common.AreEqual(testVowels, encoder.Vowels);
			Common.AreEqual(BubbleBabbleEncoder.StandardConsonants, encoder.Consonants);
			Assert.AreEqual(BubbleBabbleEncoder.StandardDivider, encoder.Divider);
			Assert.AreEqual(BubbleBabbleEncoder.StandardWrapping, encoder.Wrapping);
		}

		[Test]
		public void ConstructorVowelsConsonantsTest() {
			char[] testVowels = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			char[] testConsonants = new char[] { 'M','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder(testVowels, testConsonants);
			Common.AreEqual(testVowels, encoder.Vowels);
			Common.AreEqual(testConsonants, encoder.Consonants);
			Assert.AreEqual(BubbleBabbleEncoder.StandardDivider, encoder.Divider);
			Assert.AreEqual(BubbleBabbleEncoder.StandardWrapping, encoder.Wrapping);
		}

		[Test]
		public void ConstructorVowelsConsonantsDividerTest() {
			char[] testVowels = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			char[] testConsonants = new char[] { 'M','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			char testDivider = '?';
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder(testVowels, testConsonants, testDivider);
			Common.AreEqual(testVowels, encoder.Vowels);
			Common.AreEqual(testConsonants, encoder.Consonants);
			Assert.AreEqual(testDivider, encoder.Divider);
			Assert.AreEqual(BubbleBabbleEncoder.StandardWrapping, encoder.Wrapping);
		}

		[Test]
		public void ConstructorVowelsConsonantsDividerWrappingTest() {
			char[] testVowels = new char[] { 'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			char[] testConsonants = new char[] { 'M','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M','1','2','3','4','5','6' };
			char testDivider = '?';
			char testWrapping = '!';
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder(testVowels, testConsonants, testDivider, testWrapping);
			Common.AreEqual(testVowels, encoder.Vowels);
			Common.AreEqual(testConsonants, encoder.Consonants);
			Assert.AreEqual(testDivider, encoder.Divider);
			Assert.AreEqual(testWrapping, encoder.Wrapping);
		}


		[Test, ExpectedException(typeof(NotImplementedException))]
		public void GetDecoderTest() {
			BubbleBabbleEncoder encoder = new BubbleBabbleEncoder();
			Decoder decoder = encoder.GetDecoder();
			//Assert.IsNotNull(decoder);
			//Assert.IsInstanceOf(typeof(BubbleBabbleDecoder), decoder);
		}
	}
}
