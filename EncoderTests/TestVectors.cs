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
using System.Collections;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Classless.Encoder.Tests {
	[TestFixture]
	public class TestVectors {
		static public object[] EncodingTestVectors = {
			// http://tools.ietf.org/html/rfc4648
			new object[] { new Base16Encoder(), Encoding.ASCII.GetBytes(""), "" },
			new object[] { new Base16Encoder(), Encoding.ASCII.GetBytes("f"), "66" },
			new object[] { new Base16Encoder(), Encoding.ASCII.GetBytes("fo"), "666F" },
			new object[] { new Base16Encoder(), Encoding.ASCII.GetBytes("foo"), "666F6F" },
			new object[] { new Base16Encoder(), Encoding.ASCII.GetBytes("foob"), "666F6F62" },
			new object[] { new Base16Encoder(), Encoding.ASCII.GetBytes("fooba"), "666F6F6261" },
			new object[] { new Base16Encoder(), Encoding.ASCII.GetBytes("foobar"), "666F6F626172" },
			new object[] { new Base32Encoder(), Encoding.ASCII.GetBytes(""), "" },
			new object[] { new Base32Encoder(), Encoding.ASCII.GetBytes("f"), "MY======" },
			new object[] { new Base32Encoder(), Encoding.ASCII.GetBytes("fo"), "MZXQ====" },
			new object[] { new Base32Encoder(), Encoding.ASCII.GetBytes("foo"), "MZXW6===" },
			new object[] { new Base32Encoder(), Encoding.ASCII.GetBytes("foob"), "MZXW6YQ=" },
			new object[] { new Base32Encoder(), Encoding.ASCII.GetBytes("fooba"), "MZXW6YTB" },
			new object[] { new Base32Encoder(), Encoding.ASCII.GetBytes("foobar"), "MZXW6YTBOI======" },
			new object[] { new Base32HexEncoder(), Encoding.ASCII.GetBytes(""), "" },
			new object[] { new Base32HexEncoder(), Encoding.ASCII.GetBytes("f"), "CO======" },
			new object[] { new Base32HexEncoder(), Encoding.ASCII.GetBytes("fo"), "CPNG====" },
			new object[] { new Base32HexEncoder(), Encoding.ASCII.GetBytes("foo"), "CPNMU===" },
			new object[] { new Base32HexEncoder(), Encoding.ASCII.GetBytes("foob"), "CPNMUOG=" },
			new object[] { new Base32HexEncoder(), Encoding.ASCII.GetBytes("fooba"), "CPNMUOJ1" },
			new object[] { new Base32HexEncoder(), Encoding.ASCII.GetBytes("foobar"), "CPNMUOJ1E8======" },
			new object[] { new Base64Encoder(), Encoding.ASCII.GetBytes(""), "" },
			new object[] { new Base64Encoder(), Encoding.ASCII.GetBytes("f"), "Zg==" },
			new object[] { new Base64Encoder(), Encoding.ASCII.GetBytes("fo"), "Zm8=" },
			new object[] { new Base64Encoder(), Encoding.ASCII.GetBytes("foo"), "Zm9v" },
			new object[] { new Base64Encoder(), Encoding.ASCII.GetBytes("foob"), "Zm9vYg==" },
			new object[] { new Base64Encoder(), Encoding.ASCII.GetBytes("fooba"), "Zm9vYmE=" },
			new object[] { new Base64Encoder(), Encoding.ASCII.GetBytes("foobar"), "Zm9vYmFy" },

			// http://zooko.com/repos/z-base-32/base32/DESIGN
			new object[] { new ZBase32Encoder(), new Base32Decoder().Decode("6C74O==="), "6n9hq" },
			new object[] { new ZBase32Encoder(), new Base32Decoder().Decode("2R5AI==="), "4t7ye" },

			// http://wiki.yak.net/589/Bubble_Babble_Encoding.txt
			new object[] { new BubbleBabbleEncoder(), Encoding.ASCII.GetBytes(""), "xexax" },
			new object[] { new BubbleBabbleEncoder(), Encoding.ASCII.GetBytes("1234567890"), "xesef-disof-gytuf-katof-movif-baxux" },
			new object[] { new BubbleBabbleEncoder(), Encoding.ASCII.GetBytes("Pineapple"), "xigak-nyryk-humil-bosek-sonax" },


			new object[] { new Base64Encoder(), new byte[4] { 0xFF, 0xFF, 0xBE, 0x61 }, "//++YQ==" },
			new object[] { new Base64UrlEncoder(), new byte[4] { 0xFF, 0xFF, 0xBE, 0x61 }, "__--YQ" },
		};


		[Test, TestCaseSource("EncodingTestVectors")]
		public void EncodingTest(Encoder encoder, byte[] input, string expectedOutput) {
			string result = encoder.Encode(input);
			Assert.AreEqual(expectedOutput, result);
		}

		[Test, TestCaseSource("EncodingTestVectors")]
		public void EncodingStreamTest(Encoder encoder, byte[] input, string expectedOutput) {
			string result = encoder.Encode(new MemoryStream(input));
			Assert.AreEqual(expectedOutput, result);
		}


		[Test, TestCaseSource("EncodingTestVectors")]
		public void DecodingTest(Encoder encoder, byte[] expectedOutput, string input) {
			try {
				byte[] result = encoder.GetDecoder().Decode(input);
				Common.AreEqual(expectedOutput, result);
			} catch (NotImplementedException) { }
		}

		[Test, TestCaseSource("EncodingTestVectors")]
		public void DecodingStreamTest(Encoder encoder, byte[] expectedOutput, string input) {
			try {
				byte[] result = encoder.GetDecoder().Decode(new MemoryStream(encoder.GetOutputEncoding().GetBytes(input)));
				Common.AreEqual(expectedOutput, result);
			} catch (NotImplementedException) { }
		}

		[Test, TestCaseSource("EncodingTestVectors")]
		public void DecodingCharArrayTest(Encoder encoder, byte[] expectedOutput, string input) {
			try {
				byte[] result = encoder.GetDecoder().Decode(input.ToCharArray());
				Common.AreEqual(expectedOutput, result);
			} catch (NotImplementedException) { }
		}

		[Test, TestCaseSource("EncodingTestVectors")]
		public void DecodingCaseSensitivityTest(Encoder encoder, byte[] expectedOutput, string input) {
			try {
				Decoder decoder = encoder.GetDecoder();
				if (!decoder.IsCaseSensitive) {
					byte[] resultUpper = decoder.Decode(input.ToUpper());
					byte[] resultLower = decoder.Decode(input.ToLower());
					Common.AreEqual(resultUpper, resultLower);
				}
			} catch (NotImplementedException) { }
		}

		[Test, TestCaseSource("EncodingTestVectors")]
		public void DecodingMissingPaddingTest(Encoder encoder, byte[] expectedOutput, string input) {
			System.Reflection.PropertyInfo prop = encoder.GetType().GetProperty("Padding");
			if (prop != null) {
				try {
					byte[] result = encoder.GetDecoder().Decode(input.TrimEnd((char)prop.GetValue(encoder, null)));
					Common.AreEqual(expectedOutput, result);
				} catch (NotImplementedException) { }
			}
		}


		static public IEnumerable RandomInput {
			get {
				int iterations = 25;
				int maxInputLength = 10240;
				Random r = new Random((int)System.DateTime.Now.Ticks);

				foreach (TestCaseData testCase in Common.EncoderNames) {
					for (int i = 0; i < iterations; i++) {
						byte[] data = new byte[r.Next(maxInputLength)];
						r.NextBytes(data);
						yield return new TestCaseData(testCase.Arguments[1], data);
					}
				}
			}
		}

		[Test, TestCaseSource("RandomInput")]
		public void RandomRoundTripTest(Type encoderType, byte[] input) {
			Encoder encoder = Encoder.Create(encoderType.Name);
			string encoded = encoder.Encode(input);
			try {
				byte[] decoded = encoder.GetDecoder().Decode(encoded);
				Common.AreEqual(input, decoded);
			} catch (NotImplementedException) { }
		}

		[Test, TestCaseSource("RandomInput")]
		public void RandomRoundTripStreamTest(Type encoderType, byte[] input) {
			Encoder encoder = Encoder.Create(encoderType.Name);
			string encoded = encoder.Encode(new MemoryStream(input));
			try {
				byte[] decoded = encoder.GetDecoder().Decode(new MemoryStream(encoder.GetOutputEncoding().GetBytes(encoded)));
				Common.AreEqual(input, decoded);
			} catch (NotImplementedException) { }
		}
	}
}
