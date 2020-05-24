using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMacCurso.Models
{
    public class Pedido
    {
        [BindNever] // Informa que a propriedade PedidoId não vai ser vinculada ao formulário
        public int PedidoId { get; set; }
        public List<PedidoDetalhe> PedidoItens { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Informe o Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Informe o Sobrenome")]
        [Display(Name = "Sobrenome")]
        public string SobreNome { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Informe o Endereço")]
        [Display(Name = "Endereço")]
        public string Endereco1 { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Informe o Complemento do Endereço")]
        [Display(Name = "Complemento")]
        public string Endereco2 { get; set; }

        [StringLength(10, MinimumLength = 8)]
        [Required(ErrorMessage = "Informe o Cep")]
        [Display(Name = "Cep")]
        public string Cep { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Informe o Estado")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Informe a cidade")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [StringLength(25)]
        [Required(ErrorMessage = "Informe o Telefone")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [StringLength(80)]
        [Required(ErrorMessage = "Informe o e-mail")]
        [Display(Name = "e-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
                ErrorMessage = "O e-mail não possui um formato correto")]
        public string Email { get; set; }
        
        [BindNever] // Informa que a propriedade não vai ser vinculada ao formulário
        [ScaffoldColumn(false)] // Informa que esse campo não vai ser visível na View (não quero que seja exibido quando criar o formuário)
        [Column(TypeName = "decimal(18,2)")]
        public decimal PedidoTotal { get; set; }
        
        [BindNever]
        [ScaffoldColumn(false)]
        [Display(Name = "Data/Hora de Recebimento do Pedido")]
        //[DataType(DataType.DateTime)]
        [Column(TypeName = "Datetime")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        [Display(Name = "Data/Hora da Entrega do Pedido")]
        //[DataType(DataType.DateTime)]
        [Column(TypeName = "Datetime")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PedidoEntregueEm { get; set; }
    }
}
