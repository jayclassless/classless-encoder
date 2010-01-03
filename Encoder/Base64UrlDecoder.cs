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

namespace Classless.Encoder {
	/// <summary>An implementation of the Base64 decoding algorithm using the URL and Filename Safe alphabet.</summary>
	public class Base64UrlDecoder : Base64Decoder {
		/// <summary>Initializes a new instance of the Base64UrlDecoder class.</summary>
		public Base64UrlDecoder() : base(Base64UrlEncoder.StandardAlphabet, Base64UrlEncoder.StandardPadding) { }


		/// <summary>Returns the Encoder that corresponds with this Decoder.</summary>
		/// <returns>A new instance of the corresponding Encoder class.</returns>
		override public Encoder GetEncoder() {
			return new Base64UrlEncoder();
		}
	}
}
