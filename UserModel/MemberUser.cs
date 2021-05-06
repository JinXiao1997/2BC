using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserModel
{
    [Table("ums_member")]
    public class MemberUser
    {
        [Key]
        public long Id { get; set; }
        [Column("level_id")]
        public long LevelId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string NickName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Header { get; set; }
        public int Gender { get; set; }
        public string Birth { get; set; }
        public string City { get; set; }
        public string Job { get; set; }
        public string Sign { get; set; }
        [Column("source_type")]
        public int SourceType { get; set; }
        public int Integration { get; set; }
        public int Growth { get; set; }
        public int Status { get; set; }
        [Column("create_time")]
        public string CreateTime { get; set; }

    }
}
