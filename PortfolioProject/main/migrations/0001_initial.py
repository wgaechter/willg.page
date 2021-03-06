# Generated by Django 3.2.2 on 2021-05-27 00:31

from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Projects',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('title', models.CharField(max_length=100)),
                ('sub_title', models.CharField(max_length=100)),
                ('languages', models.CharField(max_length=100)),
                ('project_type', models.CharField(max_length=40)),
                ('description', models.TextField()),
                ('link', models.URLField()),
                ('img', models.FilePathField(path='static/img')),
            ],
        ),
    ]
