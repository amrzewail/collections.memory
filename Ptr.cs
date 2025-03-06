using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory
{

    /// <summary>
    /// A wrapper for unsafe pointer references. <br></br>
    /// Useful for Dictionary keys, comparisons and for Generics
    /// </summary>
    /// <typeparam name="T">Unmanaged type</typeparam>
    public unsafe struct Ptr<T> : IEquatable<Ptr<T>>, IEqualityComparer<Ptr<T>> where T : unmanaged
    {

        /// <summary>
        /// The unsafe pointer
        /// </summary>
        public T* Pointer { get; private set; }


        /// <summary>
        /// The value of the unsafe pointer
        /// </summary>
        public T Value => *Pointer;

        /// <summary>
        /// The value reference of the unsafe pointer
        /// </summary>
        public ref T Ref => ref *Pointer;

        /// <summary>
        /// Construct by unsafe pointer
        /// </summary>
        /// <param name="p"></param>
        public Ptr(T* p) => this.Pointer = p;

        /// <summary>
        /// Construct by a value reference
        /// </summary>
        /// <param name="value"></param>
        public Ptr(ref T value)
        {
            fixed (T* ptr = &value) this.Pointer = ptr;
        }

        /// <summary>
        /// Returns the pointer's integer address as HashCode, used for comparisons
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (Pointer is null) return 0;
            return (int)Pointer;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Ptr<T>) return false;

            return Equals((Ptr<T>)obj);
        }

        /// <summary>
        /// Compares the unsafe pointers
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Ptr<T> other)
        {
            return other.Pointer == Pointer;
        }

        /// <summary>
        /// Compares the unsafe pointers
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Ptr<T> x, Ptr<T> y)
        {
            return x.Equals(y);
        }

        /// <summary>
        /// Returns the pointer's integer address as HashCode, used for comparisons
        /// </summary>
        /// <returns></returns>
        public int GetHashCode(Ptr<T> obj)
        {
            return obj.GetHashCode();
        }

        public override string ToString()
        {
            return ((int)Pointer).ToString();
        }

        /// <summary>
        /// Checks for the invalidity of the unsafe pointer
        /// </summary>
        /// <returns></returns>
        public bool IsNull() => Pointer is null;

        public static implicit operator T*(Ptr<T> ptr) => ptr.Pointer;
        public static implicit operator T(Ptr<T> ptr) => ptr.Value;
        public static implicit operator int(Ptr<T> ptr) => (int)ptr.Pointer;

        public static bool operator ==(T* lhs, Ptr<T> rhs) => lhs == rhs.Pointer;
        public static bool operator !=(T* lhs, Ptr<T> rhs) => lhs != rhs.Pointer;
        public static bool operator ==(Ptr<T> lhs, Ptr<T> rhs) => lhs.Pointer == rhs.Pointer;
        public static bool operator !=(Ptr<T> lhs, Ptr<T> rhs) => lhs.Pointer != rhs.Pointer;
    }
}