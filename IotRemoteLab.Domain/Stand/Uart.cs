﻿using System.Text.Json.Serialization;

namespace IotRemoteLab.Domain.Stand
{
    [method: JsonConstructor]
    public readonly struct Uart(Guid id, byte index, string name)
    {
        /// <summary>
        /// Uart id.
        /// </summary>
        public Guid Id { get; } = id;
        /// <summary>
        /// Индекс порта Uart. Может назвать это порт?
        /// </summary>
        public byte Index { get; } = index;
        /// <summary>
        /// Полное название
        /// </summary>
        public string Name { get; } = name;
        public override string ToString()
        {
            return $"{Index} {Name}";
        }
    }
}