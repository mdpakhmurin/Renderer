#version 330

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

out vec4 outputColor;
in vec2 pos;

void main()
{
	vec2 newPos = pos / 0.9;
	vec3 camera_position = vec3(0, 0, 1);

	float dist = hit_sphere(camera_position, vec3(newPos,1.0));
	if (dist > 0){
		outputColor = vec4(dist, 0, 0, 1);
	}
	else{
		outputColor = vec4(abs(newPos), 0, 1);
	}
}