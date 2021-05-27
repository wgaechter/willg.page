from django.shortcuts import render
from main.models import Projects

def index(request):
    projects = Projects.objects.all()
    context = {
        'projects': projects
    }
    return render(request, "index.html", context)
