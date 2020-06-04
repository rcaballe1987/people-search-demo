using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using ClearlyAgile.TimeTracker.Shared;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using PeopleSearchAppDemo.Core.Domain;

namespace ClearlyAgile.TimeTracker.Tests
{
    public class TestSpec<T> where T : class
    {
        private Dictionary<Type, object> mocks = new Dictionary<Type, object>();

        public TType Mock<TType>()
        {
            Type type = typeof(TType);

            if (mocks.ContainsKey(type))
            {
                return (TType)mocks[type];
            }

            object mock = Substitute.For(new Type[] { type }, new object[] { });

            return (TType)mock;
        }

        public T Sut()
        {
            ConstructorInfo[] constructorInfos = typeof(T).GetConstructors();

            //find the greedy constructor
            ConstructorInfo constructorInfo = constructorInfos.OrderByDescending(x => x.GetParameters().Length).FirstOrDefault();

            //greedy constructor parameters
            ParameterInfo[] parameterInfos = constructorInfo.GetParameters();

            int length = parameterInfos.Length;
            Type[] types = new Type[length];
            var constructorParameters = new object[length];

            for (int i = 0; i < length; i++)
            {
                try
                {

                    types[i] = parameterInfos[i].ParameterType;


                    TryToCreateMock(i, constructorParameters, types[i], new object[] { });

                }
                catch (InvalidProxyConstructorArgumentsException e)
                {
                    //no parameterless constructor
                    var parameters = Create(types[i]);

                    TryToCreateMock(i, constructorParameters, types[i], parameters);

                }

            }

            // Create greedy constructor
            return (T)Activator.CreateInstance(typeof(T), constructorParameters);
        }

        private void TryToCreateMock(int index, object[] constructorParameters, Type type, object[] parameters)
        {
            object mock = Substitute.For(new Type[] { type }, parameters);

            constructorParameters[index] = mock;

            if (!mocks.ContainsKey(type))
            {
                mocks.AddRange(new Dictionary<Type, object>()
                {
                    { type, mock }
                });
            }
        }


        private object[] Create(Type type)
        {
            ConstructorInfo[] constructorInfos = type.GetConstructors();

            //find the greedy constructor
            ConstructorInfo constructorInfo = constructorInfos.OrderByDescending(x => x.GetParameters().Length).FirstOrDefault();

            //greedy constructor parameters
            ParameterInfo[] parameterInfos = constructorInfo.GetParameters();

            int length = parameterInfos.Length;
            Type[] types = new Type[length];
            var constructorParameters = new object[length];

            for (int i = 0; i < length; i++)
            {
                types[i] = parameterInfos[i].ParameterType;

                object mock = null;
                if (types[i].BaseType.Name == "DbContextOptions")
                {
                    mock = new DbContextOptions<PeopleSearchAppDemoDbContext>();
                }
                else
                {
                    mock = Substitute.For(new Type[] { types[i] }, new object[] { });
                }

                constructorParameters[i] = mock;
            }

            return constructorParameters;
        }
    }
}
