#version 430 core

uniform vec2 cameraSize;

layout(location = 0) in vec2 inPos;
out vec2 pos;


void main(void)
{
    gl_Position = vec4(inPos, 0.0, 1.0);

    pos = vec2(inPos.x * cameraSize.x * 0.5f, inPos.y * cameraSize.y * 0.5f);
}