from django.db import models

# Create your models here.

class Projects(models.Model):
    title = models.CharField(max_length=100)
    sub_title = models.CharField(max_length=100)
    languages = models.CharField(max_length=100)
    project_type = models.CharField(max_length=40)
    description = models.TextField()
    link = models.URLField()
    img = models.FilePathField(path="static/img")




