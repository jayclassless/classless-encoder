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
	/// <summary>An implementation of the Base16 encoding algorithm.</summary>
	public class Base16Encoder : Encoder {
		private StringBuilder resultBuffer = new StringBuilder();
		private char[] alphabet;


		/// <summary>The standard alphabet defined by RFC4648.</summary>
		static public readonly char[] StandardAlphabet = new char[16] {
			'0', '1', '2', '3',
			'4', '5', '6', '7',
			'8', '9', 'A', 'B',
			'C', 'D', 'E', 'F',
		};


		/// <summary>Gets or sets the alphabet used for encoding.</summary>
		/// <exception cref="ArgumentNullException">If the specified value is null.</exception>
		/// <exception cref="ArgumentException">If the specified array contains less than 16 characters.</exception>
		public char[] Alphabet {
			get { return alphabet; }
			set {
				if (value == null) {
					throw new ArgumentNullException("value", Properties.Resources.alphabetCantBeNull);
				}
				if (value.Length < 16) {
					throw new ArgumentException(Properties.Resources.alphabetTooShort, "value");
				}
				alphabet = value;
			}
		}


		/// <summary>Initializes a new instance of the Base16Encoder class.</summary>
		public Base16Encoder() : this(StandardAlphabet) { }

		/// <summary>Initializes a new instance of the Base16Encoder class.</summary>
		/// <param name="alphabet">The alphabet to use during encoding.</param>
		public Base16Encoder(char[] alphabet) {
			Alphabet = alphabet;
		}


		/// <summary>Returns the Decoder that corresponds with this Encoder.</summary>
		/// <returns>A new instance of the corresponding Decoder class.</returns>
		override public Decoder GetDecoder() {
			return new Base16Decoder();
		}


		/// <summary>Initializes the Encoder in preparation for an encoding operation.</summary>
		override public void Initialize() {
			resultBuffer = new StringBuilder();
		}


		/// <summary>Encodes an array of bytes.</summary>
		/// <param name="array">The array of bytes to encode.</param>
		/// <param name="offset">The index in the array to start encoding from.</param>
		/// <param name="length">The number of bytes in the array to encode.</param>
		override public void EncodeCore(byte[] array, int offset, int length) {
			for (int i = offset; i < (offset + length); i++) {
				resultBuffer.Append(alphabet[(uint)array[i] >> 4]);
				resultBuffer.Append(alphabet[array[i] & 0x0F]);
			}
		}


		/// <summary>Completes the encoding operation and returns the result.</summary>
		/// <returns>The result of the encoding operation.</returns>
		override public string EncodeFinal() {
			return resultBuffer.ToString();
		}
	}
}
