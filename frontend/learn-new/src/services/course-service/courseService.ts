import { combineURLs } from "@/utils";
import { Course, CreateCourseRequest, UpdateCourseRequest } from "models";

const baseURL = "https://localhost:7179/courses";

export const getAllCourses = async () => {
  const response = await fetch(baseURL, {
    method: "GET",
    credentials: "include",
  });
  const text = await response.text();
  if (text.length === 0) {
    return undefined;
  }
  const result = JSON.parse(text);
  if (result) {
    const courses = (result as any[]).map((course) => {
      return { ...course, dateCreated: new Date(course.dateCreated) };
    });
    return courses as Course[];
  }
  return undefined;
};

export const getCourseById = async (id: number) => {
  const url = new URL(combineURLs(baseURL, `/${id}`));
  const response = await fetch(url, {
    method: "GET",
    credentials: "include",
  });
  const text = await response.text();
  if (text.length === 0) {
    return undefined;
  }
  const result = JSON.parse(text);

  return { ...result, dateCreated: new Date(result.dateCreated) } as Course;
};

export const getCoursesByTeacherId = async (teacherId: string) => {
  const url = new URL(combineURLs(baseURL, `/by-teacher-id/${teacherId}`));

  const response = await fetch(url, {
    method: "GET",
    credentials: "include",
  });

  const text = await response.text();
  if (text.length === 0) {
    return undefined;
  }
  const result = JSON.parse(text);
  if (result) {
    const courses = (result as any[]).map((course) => {
      return { ...course, dateCreated: new Date(course.dateCreated) };
    });
    return courses as Course[];
  }
  return undefined;
};

export const createCourse = async (request: CreateCourseRequest) => {
  const response = await fetch(baseURL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    credentials: "include",
    body: JSON.stringify({ ...request }),
  });

  return response.ok;
};

export const updateCourse = async (request: UpdateCourseRequest) => {
  const url = new URL(combineURLs(baseURL, `/${request.id}`));

  const response = await fetch(url, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    credentials: "include",
    body: JSON.stringify({ ...request }),
  });

  return response.ok;
};
export const deleteCourse = async (id: number) => {
  const url = new URL(combineURLs(baseURL, `/${id}`));

  const response = await fetch(url, {
    method: "DELETE",
    credentials: "include",
  });

  return response.ok;
};
