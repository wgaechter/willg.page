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

class Certifications(models.Model):
    org = models.CharField(max_length=100) #IBM/Amazon Web Services
    cert_name = models.CharField(max_length=100) #Data Science Professional Certification Badge/Cloud Practitioner Certification
    synop = models.CharField(max_length=100) #Introduction to Data Science and its principles
    cert_year = models.CharField(max_length=4) #2020
    cert_month = models.CharField(max_length=10) #April
    desc = models.TextField() #Any Additional Desc. necessary 
    test_score = models.CharField(max_length=100) #if applicable (IF NOT: NA)






