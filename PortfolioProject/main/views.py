from django.shortcuts import render
from main.models import Projects
from main.models import Certifications

def index(request):
    projects = Projects.objects.all()
    certifications = Certifications.objects.all()
    context = {
        'projects': projects,
        'certifications': certifications
    }
    return render(request, "index.html", context)
