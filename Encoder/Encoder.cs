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
using System.IO;

namespace Classless.Encoder {
	/// <summary>The base class from which all encoding algorithms derive.</summary>
	abstract public class Encoder {
		/// <summary>The default size of the buffer used to read from streams.</summary>
		static public readonly int DefaultStreamBufferSize = 1024;


		/// <summary>Returns the Decoder that corresponds with this Encoder.</summary>
		/// <returns>A new instance of the corresponding Decoder class.</returns>
		abstract public Decoder GetDecoder();


		/// <summary>Returns the Encoding of the result strings.</summary>
		/// <returns>An instance of the corresponding Encoding class.</returns>
		abstract public System.Text.Encoding GetOutputEncoding();


		/// <summary>Initializes the Encoder in preparation for an encoding operation.</summary>
		abstract public void Initialize();


		/// <summary>Encodes an array of bytes.</summary>
		/// <param name="array">The array of bytes to encode.</param>
		/// <param name="offset">The index in the array to start encoding from.</param>
		/// <param name="length">The number of bytes in the array to encode.</param>
		abstract public void EncodeCore(byte[] array, int offset, int length);


		/// <summary>Completes the encoding operation and returns the result.</summary>
		/// <returns>The result of the encoding operation.</returns>
		abstract public string EncodeFinal();


		/// <summary>Encodes an array of bytes.</summary>
		/// <param name="input">The array of bytes to encode.</param>
		/// <returns>The encoded data.</returns>
		public string Encode(byte[] input) {
			return Encode(input, 0, input.Length);
		}

		/// <summary>Encodes an array of bytes.</summary>
		/// <param name="input">The array of bytes to encode.</param>
		/// <param name="offset">The index in the array to start encoding from.</param>
		/// <param name="length">The number of bytes in the array to encode.</param>
		/// <returns>The encoded data.</returns>
		public string Encode(byte[] input, int offset, int length) {
			Initialize();
			EncodeCore(input, offset, length);
			return EncodeFinal();
		}

		/// <summary>Encodes a stream of bytes.</summary>
		/// <param name="input">The stream of bytes to encode.</param>
		/// <returns>The encoded data.</returns>
		public string Encode(Stream input) {
			return Encode(input, DefaultStreamBufferSize);
		}

		/// <summary>Encodes a stream of bytes.</summary>
		/// <param name="input">The stream of bytes to encode.</param>
		/// <param name="bufferSize">The size of the buffer used to read from the stream.</param>
		/// <returns>The encoded data.</returns>
		public string Encode(Stream input, int bufferSize) {
			if (input == null) {
				throw new ArgumentNullException("input", Properties.Resources.streamCantBeNull);
			}
			if (bufferSize < 1) {
				throw new ArgumentException(Properties.Resources.invalidBufferSize, "bufferSize");
			}

			Initialize();

			int num;
			byte[] buffer = new byte[bufferSize];
			while ((num = input.Read(buffer, 0, bufferSize)) > 0) {
				EncodeCore(buffer, 0, num);
			}

			return EncodeFinal();
		}


		/// <summary>Creates an instance of the default implementation of Encoder.</summary>
		/// <returns>An instance of an Encoder object to perform the encoding operation.</returns>
		/// <remarks>The default implementation of Encoder is Base32Encoder.</remarks>
		static public Encoder Create() {
			return Create("Classless.Encoder.Base32Encoder");
		}


		/// <summary>Creates an instance of the default implementation of Encoder.</summary>
		/// <param name="name">The implementation of Encoder to create.</param>
		/// <returns>An instance of an Encoder object to perform the encoding operation.</returns>
		static public Encoder Create(string name) {
			string thisAssembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			string normalizedName = name;
			if (!normalizedName.StartsWith(thisAssembly, StringComparison.Ordinal)) {
				normalizedName = thisAssembly + "." + normalizedName;
			}

			try {
				return (Encoder)Activator.CreateInstance(thisAssembly, normalizedName).Unwrap();
			} catch (TypeLoadException) {
				return null;
			} catch (InvalidCastException) {
				return null;
			}
		}
	}
}
