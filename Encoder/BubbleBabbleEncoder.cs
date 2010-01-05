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
using System.Text;

namespace Classless.Encoder {
	/// <summary>An implementation of the BubbleBabble encoding algorithm.</summary>
	public class BubbleBabbleEncoder : BlockEncoder {
		private StringBuilder resultBuffer = new StringBuilder();
		private char[] vowels;
		private char[] consonants;
		private char divider;
		private char wrapping;
		private int seed = 1;


		/// <summary>The standard vowels.</summary>
		static public readonly char[] StandardVowels = new char[6] {
			'a', 'e', 'i', 'o',
			'u', 'y'
		};

		/// <summary>The standard consonants.</summary>
		static public readonly char[] StandardConsonants = new char[17] {
			'b', 'c', 'd', 'f',
			'g', 'h', 'k', 'l',
			'm', 'n', 'p', 'r',
			's', 't', 'v', 'z',
			'x'
		};

		/// <summary>The standard divider character.</summary>
		static public readonly char StandardDivider = '-';

		/// <summary>The standard wrapping character.</summary>
		static public readonly char StandardWrapping = 'x';


		/// <summary>Gets or sets the vowels used for encoding.</summary>
		/// <exception cref="ArgumentNullException">If the specified value is null.</exception>
		/// <exception cref="ArgumentException">If the specified array contains less than 6 characters.</exception>
		public char[] Vowels {
			get { return vowels; }
			set {
				if (value == null) {
					throw new ArgumentNullException("value", Properties.Resources.alphabetCantBeNull);
				}
				if (value.Length < 6) {
					throw new ArgumentException(Properties.Resources.alphabetTooShort, "value");
				}
				vowels = value;
			}
		}

		/// <summary>Gets or sets the consonants used for encoding.</summary>
		/// <exception cref="ArgumentNullException">If the specified value is null.</exception>
		/// <exception cref="ArgumentException">If the specified array contains less than 17 characters.</exception>
		public char[] Consonants {
			get { return consonants; }
			set {
				if (value == null) {
					throw new ArgumentNullException("value", Properties.Resources.alphabetCantBeNull);
				}
				if (value.Length < 17) {
					throw new ArgumentException(Properties.Resources.alphabetTooShort, "value");
				}
				consonants = value;
			}
		}

		/// <summary>Gets or sets the character used to divide the segments.</summary>
		public char Divider {
			get { return divider; }
			set { divider = value; }
		}

		/// <summary>Gets or sets the character used to wrap the final encoding.</summary>
		public char Wrapping {
			get { return wrapping; }
			set { wrapping = value; }
		}


		/// <summary>Initializes a new instance of the BubbleBabbleEncoder class.</summary>
		public BubbleBabbleEncoder() : this(StandardVowels, StandardConsonants, StandardDivider, StandardWrapping) { }

		/// <summary>Initializes a new instance of the BubbleBabbleEncoder class.</summary>
		/// <param name="vowels">The vowels to use during encoding.</param>
		public BubbleBabbleEncoder(char[] vowels) : this(vowels, StandardConsonants, StandardDivider, StandardWrapping) { }

		/// <summary>Initializes a new instance of the BubbleBabbleEncoder class.</summary>
		/// <param name="vowels">The vowels to use during encoding.</param>
		/// <param name="consonants">The consonants to use during encoding.</param>
		public BubbleBabbleEncoder(char[] vowels, char[] consonants) : this(vowels, consonants, StandardDivider, StandardWrapping) { }

		/// <summary>Initializes a new instance of the BubbleBabbleEncoder class.</summary>
		/// <param name="vowels">The vowels to use during encoding.</param>
		/// <param name="consonants">The consonants to use during encoding.</param>
		/// <param name="divider">The character to use to split the segments.</param>
		public BubbleBabbleEncoder(char[] vowels, char[] consonants, char divider) : this(vowels, consonants, divider, StandardWrapping) { }

		/// <summary>Initializes a new instance of the BubbleBabbleEncoder class.</summary>
		/// <param name="vowels">The vowels to use during encoding.</param>
		/// <param name="consonants">The consonants to use during encoding.</param>
		/// <param name="divider">The character to use to split the segments.</param>
		/// <param name="wrapping">The character to use to wrap the final encoding.</param>
		public BubbleBabbleEncoder(char[] vowels, char[] consonants, char divider, char wrapping) : base(2) {
			Vowels = vowels;
			Consonants = consonants;
			Divider = divider;
			Wrapping = wrapping;
		}


		/// <summary>Returns the Decoder that corresponds with this Encoder.</summary>
		/// <returns>A new instance of the corresponding Decoder class.</returns>
		override public Decoder GetDecoder() {
			// Though the specification implies decoding is possible, I'm not sure I agree.
			//return new BubbleBabbleDecoder();
			throw new NotImplementedException(Properties.Resources.decoderNotImplemented);
		}


		/// <summary>Returns the Encoding of the result strings.</summary>
		/// <returns>An instance of the corresponding Encoding class.</returns>
		override public Encoding GetOutputEncoding() {
			return Encoding.ASCII;
		}


		/// <summary>Initializes the Encoder in preparation for an encoding operation.</summary>
		override public void Initialize() {
			base.Initialize();
			resultBuffer = new StringBuilder();
			seed = 1;
		}


		/// <summary>Process a block of data.</summary>
		/// <param name="array">The block of data to process.</param>
		/// <param name="offset">Where to start in the block.</param>
		override protected void ProcessBlock(byte[] array, int offset) {
			int[] idx = new int[5];

			idx[0] = (int)((((((uint)array[offset] & 0xFF) >> 6) & 0x03) + seed) % 6);
			idx[1] = (int)((((uint)array[offset] & 0xFF) >> 2) & 0x0F);
			idx[2] = (int)(((((uint)array[offset] & 0xFF) & 0x03) + (seed / 6)) % 6);
			idx[3] = (int)((((uint)array[offset + 1] & 0xFF) >> 4) & 0x0F);
			idx[4] = (int)(((uint)array[offset + 1] & 0xFF) & 0x0F);
			seed = (int)(((seed * 5) + ((((uint)array[offset] & 0xFF) * 7) + ((uint)array[offset + 1] & 0xFF))) % 36);

			resultBuffer.Append(vowels[idx[0]]);
			resultBuffer.Append(consonants[idx[1]]);
			resultBuffer.Append(vowels[idx[2]]);
			resultBuffer.Append(consonants[idx[3]]);
			resultBuffer.Append(divider);
			resultBuffer.Append(consonants[idx[4]]);
		}
		

		/// <summary>Process the last block of data.</summary>
		/// <param name="array">The block of data to process.</param>
		/// <param name="offset">Where to start in the block.</param>
		/// <param name="length">How many bytes need to be processed.</param>
		/// <returns>The results of the completed encoding operation.</returns>
		override protected string ProcessFinalBlock(byte[] array, int offset, int length) {
			if (length == 1) {
				int[] idx = new int[3];

				idx[0] = (int)((((((uint)array[offset] & 0xFF) >> 6) & 0x03) + seed) % 6);
				idx[1] = (int)((((uint)array[offset] & 0xFF) >> 2) & 0x0F);
				idx[2] = (int)(((((uint)array[offset] & 0xFF) & 0x03) + (seed / 6)) % 6);

				resultBuffer.Append(vowels[idx[0]]);
				resultBuffer.Append(consonants[idx[1]]);
				resultBuffer.Append(vowels[idx[2]]);
			} else {
				resultBuffer.Append(vowels[seed % 6]);
				resultBuffer.Append(consonants[16]);
				resultBuffer.Append(vowels[seed / 6]);
			}

			return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}{0}", wrapping, resultBuffer.ToString());
		}
	}
}
