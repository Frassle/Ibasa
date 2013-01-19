﻿#region --- OpenTK.OpenAL License ---
/* AlcTokens.cs
 * C header: \OpenAL 1.1 SDK\include\Alc.h
 * Spec: http://www.openal.org/openal_webstf/specs/OpenAL11Specification.pdf
 * Copyright (c) 2008 Christoph Brandtner and Stefanos Apostolopoulos
 * See license.txt for license details
 * http://www.OpenTK.net */
#endregion

using System;

namespace OpenTK.Audio.OpenAL
{
    /// <summary>
    /// Defines available context attributes.
    /// </summary>
    public enum AlcContextAttributes : int
    {
        ///<summary>Followed by System.Int32 Hz</summary>
        Frequency = 0x1007,

        ///<summary>Followed by System.Int32 Hz</summary>
        Refresh = 0x1008,

        ///<summary>Followed by AlBoolean.True, or AlBoolean.False</summary>
        Sync = 0x1009,

        ///<summary>Followed by System.Int32 Num of requested Mono (3D) Sources</summary>
        MonoSources = 0x1010,

        ///<summary>Followed by System.Int32 Num of requested Stereo Sources</summary>
        StereoSources = 0x1011,

        /// <summary>(EFX Extension) This Context property can be passed to OpenAL during Context creation (alcCreateContext) to request a maximum number of Auxiliary Sends desired on each Source. It is not guaranteed that the desired number of sends will be available, so an application should query this property after creating the context using alcGetIntergerv. Default: 2</summary>
        EfxMaxAuxiliarySends = 0x20003,
    }

    /// <summary>
    /// Defines OpenAL context errors.
    /// </summary>
    public enum AlcError : int
    {
        ///<summary>There is no current error.</summary>
        NoError = 0,

        ///<summary>No Device. The device handle or specifier names an inaccessible driver/server.</summary>
        InvalidDevice = 0xA001,

        ///<summary>Invalid context ID. The Context argument does not name a valid context.</summary>
        InvalidContext = 0xA002,

        ///<summary>Bad enum. A token used is not valid, or not applicable.</summary>
        InvalidEnum = 0xA003,

        ///<summary>Bad value. A value (e.g. Attribute) is not valid, or not applicable.</summary>
        InvalidValue = 0xA004,

        ///<summary>Out of memory. Unable to allocate memory.</summary>
        OutOfMemory = 0xA005,
    }

    /// <summary>
    /// Defines available parameters for <see cref="Alc.GetString(IntPtr, AlcGetStringList)"/>.
    /// </summary>
    public static class GetString
    {
        ///<summary>The specifier string for the default device.</summary>
        public const int DefaultDeviceSpecifier = 0x1004;

        ///<summary>A list of available context extensions separated by spaces.</summary>
        public const int Extensions = 0x1006;

        ///<summary>The name of the default capture device</summary>
        public const int CaptureDefaultDeviceSpecifier = 0x311; // ALC_EXT_CAPTURE extension.

        /// <summary>a list of the default devices.</summary>
        public const int DefaultAllDevicesSpecifier = 0x1012;

        ///<summary>The name of the specified capture device, or a list of all available capture devices if no capture device is specified. ALC_EXT_CAPTURE_EXT </summary>
        public const int CaptureDeviceSpecifier = 0x310;

        ///<summary>The specifier strings for all available devices. ALC_ENUMERATION_EXT</summary>
        public const int DeviceSpecifier = 0x1005;

        /// <summary>The specifier strings for all available devices. ALC_ENUMERATE_ALL_EXT</summary>
        public const int AllDevicesSpecifier = 0x1013;
    }

    /// <summary>
    /// Defines available parameters for <see cref="Alc.GetInteger(IntPtr, AlcGetInteger, int, int[])"/>.
    /// </summary>
    public static class GetInteger
    {
        ///<summary>The specification revision for this implementation (major version). NULL is an acceptable device.</summary>
        public const int MajorVersion = 0x1000;

        ///<summary>The specification revision for this implementation (minor version). NULL is an acceptable device.</summary>
        public const int MinorVersion = 0x1001;

        ///<summary>The size (number of ALCint values) required for a zero-terminated attributes list, for the current context. NULL is an invalid device.</summary>
        public const int AttributesSize = 0x1002;

        ///<summary>Expects a destination of ALC_ATTRIBUTES_SIZE, and provides an attribute list for the current context of the specified device. NULL is an invalid device.</summary>
        public const int AllAttributes = 0x1003;

        ///<summary>The number of capture samples available. NULL is an invalid device.</summary>
        public const int CaptureSamples = 0x312;
    }
}
