# BoneHost

BoneHost is java script web application.

## Quickstart

Just clone repo, build bonehost image and run bonehost container.

```
git clone https://github.com/alexander-wurl/BoneHost
cd bonehost
docker build -t bonehost .
docker run -d -p 8080:80 bonehost
```

To test bonehost open localhost:8080 with a web browser.
![bonehost](https://github.com/alexander-wurl/BoneHost/blob/main/bonehost.png)