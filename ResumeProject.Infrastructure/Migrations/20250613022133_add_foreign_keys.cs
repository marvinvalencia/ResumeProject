// <copyright file="20250613022133_add_foreign_keys.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

#pragma warning disable SA0001, SA1633, SA1300, SA1413, SA1200

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_foreign_keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Resume_ResumeId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Resume_ResumeId",
                table: "Skill");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResumeId",
                table: "Skill",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ResumeId",
                table: "Experience",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ResumeId",
                table: "Education",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Resume_ResumeId",
                table: "Experience",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Resume_ResumeId",
                table: "Skill",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Resume_ResumeId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Resume_ResumeId",
                table: "Skill");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResumeId",
                table: "Skill",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResumeId",
                table: "Experience",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResumeId",
                table: "Education",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Resume_ResumeId",
                table: "Experience",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Resume_ResumeId",
                table: "Skill",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");
        }
    }
}

#pragma warning disable SA0001, SA1633, SA1300, SA1413, SA1200