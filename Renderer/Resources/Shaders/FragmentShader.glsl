﻿#version 430 core

//uniform sampler3D grid;
uniform vec3 cameraPosition;

layout(std430, binding = 3) buffer grid
{
    int data_SSBO[];
};

in vec2 pos;
out vec4 outputColor;


float hit_sphere(vec3 ray_start_position, vec3 ray_direction) {
	vec3 sphere_center = vec3(0, 0, 3);
	vec3 oc = ray_start_position - sphere_center;
	float a = dot(ray_direction, ray_direction);
	float b = 2 * dot(oc, ray_direction);
	float c = dot(oc, oc) - 0.2;
	float discriminant = b * b - 4 * a * c;
	if (discriminant < 0) {
		return -1.0;
	}
	else {
		return (-b - sqrt(discriminant)) / (2.0*a);
	}
}

void main()
{
	vec2 newPos = pos / 0.9;
	float nearPlaneDistance = 0;
	float farPlaneDistance = 4;
	float farScale = 2;

	vec3 diretction = vec3(newPos * farScale - newPos, farPlaneDistance - nearPlaneDistance);
//	vec4 fe = texture(grid, vec3(0,0,0));
	outputColor = vec4(data_SSBO[0], 0, 1, 1);// vec4(x.x,1,1,1);
//	outputColor = vec4(1,fe.x,0,0);

//
//	float dist = hit_sphere(cameraPosition, diretction);
//	if (dist > 0){
//		outputColor = vec4(dist, 0, 0, 1);
//	}
//	else{
//		outputColor = vec4(abs(newPos), 0, 1);
//	}
}