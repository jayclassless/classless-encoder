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
using System.IO;

namespace Classless.Encoder {
	/// <summary>The base class from which all decoding algorithms derive.</summary>
	abstract public class Decoder {
		/// <summary>The default size of the buffer used to read from streams.</summary>
		static public readonly int DefaultStreamBufferSize = 1024;


		/// <summary>Gets a value indicating whether the decoding is case sensitive.</summary>
		abstract public bool IsCaseSensitive { get; }


		/// <summary>Returns the Encoder that corresponds with this Decoder.</summary>
		/// <returns>A new instance of the corresponding Encoder class.</returns>
		abstract public Encoder GetEncoder();


		/// <summary>Initializes the Decoder in preparation for a decoding operation.</summary>
		abstract public void Initialize();


		/// <summary>Decodes an array of characters.</summary>
		/// <param name="array">The array of characters to decode.</param>
		/// <param name="offset">The index in the array to start decoding from.</param>
		/// <param name="length">The number of characters in the array to decode.</param>
		abstract public void DecodeCore(char[] array, int offset, int length);


		/// <summary>Completes the decoding operation and returns the result.</summary>
		/// <returns>The result of the decoding operation.</returns>
		abstract public byte[] DecodeFinal();


		/// <summary>Decodes an array of characters.</summary>
		/// <param name="input">The array of characters to decode.</param>
		/// <returns>The decoded data.</returns>
		public byte[] Decode(char[] input) {
			return Decode(input, 0, input.Length);
		}

		/// <summary>Decodes an array of characters.</summary>
		/// <param name="input">The array of characters to decode.</param>
		/// <param name="offset">The index in the array to start decoding from.</param>
		/// <param name="length">The number of characters in the array to decode.</param>
		/// <returns>The decoded data.</returns>
		public byte[] Decode(char[] input, int offset, int length) {
			Initialize();
			DecodeCore(input, offset, length);
			return DecodeFinal();
		}

		/// <summary>Decodes a string.</summary>
		/// <param name="input">The string to decode.</param>
		/// <returns>The decoded data.</returns>
		public byte[] Decode(string input) {
			return Decode(input, 0, input.Length);
		}

		/// <summary>Decodes a string.</summary>
		/// <param name="input">The string to decode.</param>
		/// <param name="offset">The index in the string to start decoding from.</param>
		/// <param name="length">The number of characters in the string to decode.</param>
		/// <returns>The decoded data.</returns>
		public byte[] Decode(string input, int offset, int length) {
			char[] charArray = input.ToCharArray(offset, length);
			Initialize();
			DecodeCore(charArray, 0, charArray.Length);
			return DecodeFinal();
		}

		/// <summary>Decodes a stream of characters.</summary>
		/// <param name="input">The stream of characters to decode.</param>
		/// <returns>The decoded data.</returns>
		public byte[] Decode(Stream input) {
			return Decode(input, DefaultStreamBufferSize);
		}

		/// <summary>Decodes a stream of characters.</summary>
		/// <param name="input">The stream of characters to decode.</param>
		/// <param name="bufferSize">The size of the buffer used to read from the stream.</param>
		/// <returns>The decoded data.</returns>
		public byte[] Decode(Stream input, int bufferSize) {
			if (input == null) {
				throw new ArgumentNullException("input", Properties.Resources.streamCantBeNull);
			}
			if (bufferSize < 1) {
				throw new ArgumentException(Properties.Resources.invalidBufferSize, "bufferSize");
			}

			Initialize();

			int num;
			char[] temp;
			byte[] buffer = new byte[bufferSize];
			while ((num = input.Read(buffer, 0, bufferSize)) > 0) {
				temp = System.Text.Encoding.ASCII.GetChars(buffer, 0, num);
				DecodeCore(temp, 0, temp.Length);
			}

			return DecodeFinal();
		}


		/// <summary>Creates an instance of the default implementation of Decoder.</summary>
		/// <returns>An instance of an Decoder object to perform the decoding operation.</returns>
		/// <remarks>The default implementation of Decoder is Base32Decoder.</remarks>
		static public Decoder Create() {
			return Create("Classless.Encoder.Base32Decoder");
		}


		/// <summary>Creates an instance of the default implementation of Decoder.</summary>
		/// <param name="name">The implementation of Decoder to create.</param>
		/// <returns>An instance of an Decoder object to perform the decoding operation.</returns>
		static public Decoder Create(string name) {
			string thisAssembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			string normalizedName = name;
			if (!normalizedName.StartsWith(thisAssembly, StringComparison.Ordinal)) {
				normalizedName = thisAssembly + "." + normalizedName;
			}

			try {
				return (Decoder)Activator.CreateInstance(thisAssembly, normalizedName).Unwrap();
			} catch (TypeLoadException) {
				return null;
			} catch (InvalidCastException) {
				return null;
			}
		}
	}
}
