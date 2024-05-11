﻿namespace Inception.Domain;

public class Movimentacao
{
    public int Id { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataMovimentacao { get; set; }
    public ContaBancaria Conta { get; set; }
}